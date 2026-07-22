namespace Model
{
    public abstract class ClassificationModel
    {
        public abstract string Predict(Dictionary<string, string> sample);
    }
    public class NaiveBayesModel : ClassificationModel
    {
        public List<string> Labels { get; }
        public Dictionary<string, double> Priors { get; }
        public Dictionary<(string Label, string Feature, string Value), double> Cond { get; }
        public Dictionary<(string Label, string Feature), double> Unseen { get; }
        public NaiveBayesModel(List<Dictionary<string, string>> rows, string targetColumn)
        {
            List<string> labels = rows.Select(row => row[targetColumn]).Distinct().ToList();

            Dictionary<string, double> priors = rows.GroupBy(row => row[targetColumn]).ToDictionary(g => g.Key, g => (double)g.Count() / rows.Count);

            Dictionary<(string Label, string Feature, string value), double> cond = new();
            Dictionary<(string Label, string Feature), double> unseen = new();
            foreach (string label in labels)
            {
                var labelRows = rows.Where(r => r[targetColumn] == label).ToList();
                foreach (string feature in rows[0].Keys.Where(k => k != targetColumn))
                {
                    var values = rows.Select(r => r[feature]).Distinct().ToList();
                    foreach (string value in values)
                    {
                        int matching = labelRows.Count(r => r[feature] == value);
                        cond[(label, feature, value)] = (double)(matching + 1) / (labelRows.Count + values.Count);
                    }
                    unseen[(label, feature)] = 1.0 / (labelRows.Count + values.Count);
                }
            }
            Labels = labels;
            Priors = priors;
            Cond = cond;
            Unseen = unseen;
        }
        public override string Predict(Dictionary<string, string> sample)
        {
            string? bestLabel = null;
            double bestScore = double.MinValue;
            foreach (string label in Labels)
            {
                if (Priors.ContainsKey(label))
                {
                    double score = Priors[label];
                    foreach (var (feature, value) in sample)
                    {
                        if (Cond.ContainsKey((label, feature, value)))
                        {
                            score = score * Cond[(label, feature, value)];
                        }
                        else
                        {
                            score = score * Unseen[(label, feature)];
                        }
                    }

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestLabel = label;
                    }
                }
            }
            return bestLabel ?? string.Empty;
        }
    }
}