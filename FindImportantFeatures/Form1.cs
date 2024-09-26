using Microsoft.ML;
using Microsoft.ML.Data;
using System.Collections.Immutable;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace FindImportantFeatures
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUsePFI_Click(object sender, EventArgs e)
        {
            MLContext mlContext = new MLContext();
            // ���J���
            IDataView data = mlContext.Data.LoadFromTextFile<Patient>("diabetes.csv", separatorChar: ',', hasHeader: true);

            // �N��ƥH80%:20%����Ҥ��Φ��V�m��ƩM���ո��
            var trainTestData = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            //���o���Χ����V�m���
            var trainData = trainTestData.TrainSet;
            //���o���Χ������ո��
            var testData = trainTestData.TestSet;

            //�ǳƱ����R���S�x(���X�Ҧ����FOutcome�H�~���Ҧ��S�x)
            var featureColumns = typeof(Patient).GetProperties()
                    .Where(p => p.Name != nameof(Patient.Outcome))
                    .Select(p => p.Name)
                    .ToArray();

            // ���w�����R���S�x, �վ�S�x�����e�Ȧ�0~1�������d��, �ë��w�ϥ�Sdca(Stochastic Dual Coordinate Ascent)�j�k�t��k
            var pipeline = mlContext.Transforms.Concatenate("Features",  featureColumns)
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Regression.Trainers.Sdca());

            // �ϥΰV�m��ư���V�m
            var model = pipeline.Fit(trainData);

            // �ϥδ��ո�ư������
            var predictions = model.Transform(testData);
            
            // ���o�S�x�����n�ʼƾ�
            ImmutableDictionary<string, RegressionMetricsStatistics> permutationFeatureImportance =
                                    mlContext.Regression.PermutationFeatureImportance(model, predictions, permutationCount: 3);
            // �ƧǯS�x�����n�ʼƾ�
            var featureImportanceMetrics =  permutationFeatureImportance
                    .Select((metric, index) => new { index, metric.Value.RSquared })
                    .OrderByDescending(myFeatures => Math.Abs(myFeatures.RSquared.Mean));
            //  ��ܯS�x��R2(���n��)��������
            Trace.WriteLine("Feature\tPFI");
            foreach (var feature in featureImportanceMetrics)
            {
                Trace.WriteLine($"Column:{featureColumns[feature.index]}, {feature.RSquared.Mean:F6}");
            }
        }
    }
}
