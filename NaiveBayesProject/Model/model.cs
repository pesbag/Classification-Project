using System.Collections.Generic;
namespace Model
{

    public class NaiveBayesModel
    {
        public List<string> Labels { get; }

        public Dictionary<string, double> Priors { get; }

        public Dictionary<(string Label, string Feature, string Value), double> Cond { get; }

        public Dictionary<(string Label, string Feature), double> Unseen { get; }

        public NaiveBayesModel(List<Dictionary<string, string>> rows, string targetColumn)
        {
            int n = rows.Count;

            List<string> labels = rows.Select(row => row[targetColumn]).Distinct().ToList();

            Dictionary<string, double> priors = rows.GroupBy(row => row[targetColumn]).ToDictionary(g => g.Key,g => (double)g.Count() / n);

            Dictionary<(string Label, string Feature, string value), double> cond = new();
            Dictionary<(string Label, string Feature), double> unseen = new();
            foreach (string label in labels)
            {
                var labelRows = rows.Where(r => r[targetColumn] == label).ToList();
                int labelCount = labelRows.Count;
                foreach (string feature in rows[0].Keys.Where(k => k != targetColumn))
                {
                    var values = rows.Select(r => r[feature]).Distinct().ToList();
                    int distinct = values.Count;
                    foreach (string value in values)
                    {
                        int match = labelRows.Count(r => r[feature] == value);
                        cond[(label, feature, value)] = (double)(match + 1) / (labelCount + distinct);
                    }
                    unseen[(label, feature)] =  1.0 / (labelCount + distinct);
                }
            }
            Labels = labels;
            Priors= priors;
            Cond = cond;
            Unseen = unseen;
        }

       
        public string Predict(Dictionary<string, string> sample)
        {

        }
    }
}