using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindImportantFeatures
{
    public class Patient
    {
        [ColumnName("Pregnancies"), LoadColumn(0)]
        public float Pregnancies { get; set; }

        [ColumnName("Glucose"), LoadColumn(1)]
        public float Glucose { get; set; }

        [ColumnName("BloodPressure"), LoadColumn(2)]
        public float BloodPressure { get; set; }
            
        [ColumnName("SkinThickness"), LoadColumn(3)]
        public float SkinThickness {  get; set; }

        [ColumnName("Insulin"), LoadColumn(4)]
        public float Insulin { get; set; }
           
        [ColumnName("BMI"), LoadColumn(5)]
        public float BMI {  get; set; }

        [ColumnName("DiabetesPedigreeFunction"), LoadColumn(6)]
        public float DiabetesPedigreeFunction {  get; set; }

        [ColumnName("Age"), LoadColumn(7)]
        public float Age { get; set; }

        [ColumnName("Label"), LoadColumn(8)]        //必須將結果欄位的欄位名稱命名為Label
        public float Outcome { get; set; }
    }
}
