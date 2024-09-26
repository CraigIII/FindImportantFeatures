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
            // 載入資料
            IDataView data = mlContext.Data.LoadFromTextFile<Patient>("diabetes.csv", separatorChar: ',', hasHeader: true);

            // 將資料以80%:20%的比例切割成訓練資料和測試資料
            var trainTestData = mlContext.Data.TrainTestSplit(data, testFraction: 0.2);
            //取得切割妥的訓練資料
            var trainData = trainTestData.TrainSet;
            //取得切割妥的測試資料
            var testData = trainTestData.TestSet;

            //準備欲分析的特徵(取出所有除了Outcome以外的所有特徵)
            var featureColumns = typeof(Patient).GetProperties()
                    .Where(p => p.Name != nameof(Patient.Outcome))
                    .Select(p => p.Name)
                    .ToArray();

            // 指定欲分析的特徵, 調整特徵的內容值至0~1之間的範圍, 並指定使用Sdca(Stochastic Dual Coordinate Ascent)迴歸演算法
            var pipeline = mlContext.Transforms.Concatenate("Features",  featureColumns)
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Regression.Trainers.Sdca());

            // 使用訓練資料執行訓練
            var model = pipeline.Fit(trainData);

            // 使用測試資料執行測試
            var predictions = model.Transform(testData);
            
            // 取得特徵的重要性數據
            ImmutableDictionary<string, RegressionMetricsStatistics> permutationFeatureImportance =
                                    mlContext.Regression.PermutationFeatureImportance(model, predictions, permutationCount: 3);
            // 排序特徵的重要性數據
            var featureImportanceMetrics =  permutationFeatureImportance
                    .Select((metric, index) => new { index, metric.Value.RSquared })
                    .OrderByDescending(myFeatures => Math.Abs(myFeatures.RSquared.Mean));
            //  顯示特徵的R2(重要性)的平均值
            Trace.WriteLine("Feature\tPFI");
            foreach (var feature in featureImportanceMetrics)
            {
                Trace.WriteLine($"Column:{featureColumns[feature.index]}, {feature.RSquared.Mean:F6}");
            }
        }
    }
}
