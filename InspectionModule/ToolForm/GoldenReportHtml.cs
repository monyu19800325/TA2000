using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TA2000Modules
{
    public partial class GoldenReport
    {
        private string BuildSectionCard(string title, string subTitle, string bodyHtml)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<section class='card'>");
            sb.AppendLine("  <div class='card-h'>");
            sb.AppendLine("    <div>");
            sb.AppendLine("      <div class='h2'>" + HtmlUtil.H(title) + "</div>");
            sb.AppendLine("      <div class='sub'>" + HtmlUtil.H(subTitle) + "</div>");
            sb.AppendLine("    </div>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class='body'>");
            sb.AppendLine(bodyHtml);
            sb.AppendLine("  </div>");
            sb.AppendLine("</section>");

            return sb.ToString();
        }

        private string BuildCirclePitchRawSectionHtml()
        {
            List<double>[] src = new List<double>[] { CirclePitch_1_1, CirclePitch_1_2, CirclePitch_1_3, CirclePitch_1_4, CirclePitch_1_5,
                               CirclePitch_2_1, CirclePitch_2_2, CirclePitch_2_3, CirclePitch_2_4, CirclePitch_2_5 };

            var all = new List<double>();
            for (int i = 0; i < src.Length; i++)
                if (src[i] != null) all.AddRange(src[i]);


            var sb = new StringBuilder();


            sb.AppendLine("<div style='margin-top:12px;'>");
            sb.AppendLine(BuildCirclePitchRawHtml());
            sb.AppendLine("</div>");

            return sb.ToString();
        }

        private string BuildCirclePitchAccuracySectionHtml()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<table class='table'>");
            sb.AppendLine("  <thead>");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <th>CRITERION</th>");
            sb.AppendLine("      <th class='num'>Circle Pitch P - P</th>");
            sb.AppendLine("      <th class='num'>Circle Pitch STD</th>");
            sb.AppendLine("      <th class='num'>SPECIFIED</th>");
            sb.AppendLine("      <th class='num'>STATUS</th>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("  </thead>");
            sb.AppendLine("  <tbody>");


            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_1_1, "Standard"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_1_2, "+20"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_1_3, "+40"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_1_4, "+60"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_1_5, "+80"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_2_1, "-20"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_2_2, "-40"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_2_3, "-60"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_2_4, "-80"));
            sb.AppendLine(CalcCirclePitchRowHtml(CirclePitch_2_5, "-100"));


            sb.AppendLine("  </tbody>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }

        private string BuildCircleDiameterRawSectionHtml()
        {
            var all = new List<double>();

            List<List<double>>[] src = new List<List<double>>[]
                {
            CircleDiameter_1_1, CircleDiameter_1_2, CircleDiameter_1_3, CircleDiameter_1_4, CircleDiameter_1_5,
            CircleDiameter_2_1, CircleDiameter_2_2, CircleDiameter_2_3, CircleDiameter_2_4, CircleDiameter_2_5
                };

            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == null) continue;
                for (int j = 0; j < src[i].Count; j++)
                {
                    if (src[i][j] != null) all.AddRange(src[i][j]);
                }
            }

            var sb = new StringBuilder();


            sb.AppendLine("<div style='margin-top:12px;'>");
            sb.AppendLine(BuildCircleDiameterRawHtml());
            sb.AppendLine("</div>");

            return sb.ToString();
        }

        private string BuildCircleDiameterAccuracySectionHtml()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<table class='table'>");
            sb.AppendLine("  <thead>");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <th>CRITERION</th>");
            sb.AppendLine("      <th>Ball</th>");
            sb.AppendLine("      <th class='num'>Circle Diameter P - P</th>");
            sb.AppendLine("      <th class='num'>Circle Diameter STD</th>");
            sb.AppendLine("      <th class='num'>SPECIFIED</th>");
            sb.AppendLine("      <th class='num'>STATUS</th>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("  </thead>");
            sb.AppendLine("  <tbody>");

            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_1[0], "Standard", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_1[1], "Standard", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_2[0], "+20", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_2[1], "+20", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_3[0], "+40", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_3[1], "+40", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_4[0], "+60", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_4[1], "+60", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_5[0], "+80", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_1_5[1], "+80", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_1[0], "-20", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_1[1], "-20", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_2[0], "-40", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_2[1], "-40", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_3[0], "-60", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_3[1], "-60", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_4[0], "-80", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_4[1], "-80", "2"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_5[0], "-100", "1"));
            sb.AppendLine(CalcCircleDiameterRowHtml(CircleDiameter_2_5[1], "-100", "2"));


            sb.AppendLine("  </tbody>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }

        private string BuildPitchChartsHtml()
        {
            var labels = new List<string>
    {
        "Standard","+20","+40","+60","+80",
        "-20","-40","-60","-80","-100"
    };

            List<double>[] src = new List<double>[] { CirclePitch_1_1, CirclePitch_1_2, CirclePitch_1_3, CirclePitch_1_4, CirclePitch_1_5,
                               CirclePitch_2_1, CirclePitch_2_2, CirclePitch_2_3, CirclePitch_2_4, CirclePitch_2_5 };

            var avgList = new List<double>();
            var p2pList = new List<double>();

            for (int i = 0; i < src.Length; i++)
            {
                avgList.Add(SafeAverage(src[i]));
                p2pList.Add(SafeP2P(src[i]));
            }

            string avgCanvasId = "pitchAvgChart";
            string p2pCanvasId = "pitchP2PChart";

            var sb = new StringBuilder();

            sb.AppendLine("<div class='chart-grid'>");
            sb.AppendLine("  <div class='chart-box'>");
            sb.AppendLine("    <div class='chart-title'>Circle Pitch 平均值</div>");
            sb.AppendLine("    <canvas id='" + avgCanvasId + "' height='120'></canvas>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class='chart-box'>");
            sb.AppendLine("    <div class='chart-title'>Circle Pitch P-P</div>");
            sb.AppendLine("    <canvas id='" + p2pCanvasId + "' height='120'></canvas>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<script>");
            sb.AppendLine("new Chart(document.getElementById('" + avgCanvasId + "'), {");
            sb.AppendLine(" type: 'bar',");
            sb.AppendLine(" data: {");
            sb.AppendLine("   labels: " + JsArrayString(labels) + ",");
            sb.AppendLine("   datasets: [{");
            sb.AppendLine("     label: 'Average',");
            sb.AppendLine("     data: " + JsArrayDouble(avgList));
            sb.AppendLine("   }]");
            sb.AppendLine(" },");
            sb.AppendLine(" options: { responsive:true, plugins:{ legend:{ display:true } }, scales:{ y:{ beginAtZero:false } } }");
            sb.AppendLine("});");

            sb.AppendLine("new Chart(document.getElementById('" + p2pCanvasId + "'), {");
            sb.AppendLine(" type: 'bar',");
            sb.AppendLine(" data: {");
            sb.AppendLine("   labels: " + JsArrayString(labels) + ",");
            sb.AppendLine("   datasets: [{");
            sb.AppendLine("     label: 'P-P',");
            sb.AppendLine("     data: " + JsArrayDouble(p2pList));
            sb.AppendLine("   }]");
            sb.AppendLine(" },");
            sb.AppendLine(" options: { responsive:true, plugins:{ legend:{ display:true } }, scales:{ y:{ beginAtZero:true } } }");
            sb.AppendLine("});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        private string BuildDiameterChartsHtml()
        {
            var labels = new List<string>();
            var avgList = new List<double>();
            var p2pList = new List<double>();

            List<List<double>>[] src = new List<List<double>>[]
                {
            CircleDiameter_1_1, CircleDiameter_1_2, CircleDiameter_1_3, CircleDiameter_1_4, CircleDiameter_1_5,
            CircleDiameter_2_1, CircleDiameter_2_2, CircleDiameter_2_3, CircleDiameter_2_4, CircleDiameter_2_5
                };

            string[] baseKeys = new string[]
            {
        "Standard","+20","+40","+60","+80",
        "-20","-40","-60","-80","-100"
            };

            for (int i = 0; i < src.Length; i++)
            {
                for (int b = 0; b < 2; b++)
                {
                    labels.Add(baseKeys[i] + "_" + (b + 1));

                    if (src[i] != null && src[i].Count > b)
                    {
                        avgList.Add(SafeAverage(src[i][b]));
                        p2pList.Add(SafeP2P(src[i][b]));
                    }
                    else
                    {
                        avgList.Add(double.NaN);
                        p2pList.Add(double.NaN);
                    }
                }
            }

            string avgCanvasId = "diameterAvgChart";
            string p2pCanvasId = "diameterP2PChart";

            var sb = new StringBuilder();

            sb.AppendLine("<div class='chart-grid'>");
            sb.AppendLine("  <div class='chart-box'>");
            sb.AppendLine("    <div class='chart-title'>Circle Diameter 平均值</div>");
            sb.AppendLine("    <canvas id='" + avgCanvasId + "' height='120'></canvas>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class='chart-box'>");
            sb.AppendLine("    <div class='chart-title'>Circle Diameter P-P</div>");
            sb.AppendLine("    <canvas id='" + p2pCanvasId + "' height='120'></canvas>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<script>");
            sb.AppendLine("new Chart(document.getElementById('" + avgCanvasId + "'), {");
            sb.AppendLine(" type: 'bar',");
            sb.AppendLine(" data: {");
            sb.AppendLine("   labels: " + JsArrayString(labels) + ",");
            sb.AppendLine("   datasets: [{");
            sb.AppendLine("     label: 'Average',");
            sb.AppendLine("     data: " + JsArrayDouble(avgList));
            sb.AppendLine("   }]");
            sb.AppendLine(" },");
            sb.AppendLine(" options: {");
            sb.AppendLine("   responsive:true,");
            sb.AppendLine("   plugins:{ legend:{ display:true } },");
            sb.AppendLine("   scales:{");
            sb.AppendLine("     x:{");
            sb.AppendLine("       ticks:{ autoSkip:false, maxRotation:45, minRotation:45 }");
            sb.AppendLine("     },");
            sb.AppendLine("     y:{ beginAtZero:false }");
            sb.AppendLine("   }");
            sb.AppendLine(" }");
            sb.AppendLine("});");

            sb.AppendLine("new Chart(document.getElementById('" + p2pCanvasId + "'), {");
            sb.AppendLine(" type: 'bar',");
            sb.AppendLine(" data: {");
            sb.AppendLine("   labels: " + JsArrayString(labels) + ",");
            sb.AppendLine("   datasets: [{");
            sb.AppendLine("     label: 'P-P',");
            sb.AppendLine("     data: " + JsArrayDouble(p2pList));
            sb.AppendLine("   }]");
            sb.AppendLine(" },");
            sb.AppendLine(" options: {");
            sb.AppendLine("   responsive:true,");
            sb.AppendLine("   plugins:{ legend:{ display:true } },");
            sb.AppendLine("   scales:{");
            sb.AppendLine("     x:{");
            sb.AppendLine("       ticks:{ autoSkip:false, maxRotation:45, minRotation:45 }");
            sb.AppendLine("     },");
            sb.AppendLine("     y:{ beginAtZero:false }");
            sb.AppendLine("   }");
            sb.AppendLine(" }");
            sb.AppendLine("});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        private double SafeAverage(List<double> data)
        {
            if (data == null || data.Count == 0) return double.NaN;
            return data.Average();
        }

        private double SafeP2P(List<double> data)
        {
            if (data == null || data.Count == 0) return double.NaN;
            return GetMaxSubMin(data.Max(), data.Min());
        }

        private string JsArrayString(List<string> values)
        {
            return "[" + string.Join(",", values.Select(x => "'" + x.Replace("'", "\\'") + "'")) + "]";
        }

        private string JsArrayDouble(List<double> values)
        {
            return "[" + string.Join(",", values.Select(x =>
                double.IsNaN(x) ? "null" : x.ToString(System.Globalization.CultureInfo.InvariantCulture))) + "]";
        }

        private string BuildTabScript()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<script>");
            sb.AppendLine("function openTab(tabId, btn){");
            sb.AppendLine("  var panels = document.getElementsByClassName('tab-panel');");
            sb.AppendLine("  for(var i=0;i<panels.length;i++){ panels[i].classList.remove('active'); }");
            sb.AppendLine("  var buttons = document.getElementsByClassName('tab-btn');");
            sb.AppendLine("  for(var j=0;j<buttons.length;j++){ buttons[j].classList.remove('active'); }");
            sb.AppendLine("  document.getElementById(tabId).classList.add('active');");
            sb.AppendLine("  btn.classList.add('active');");
            sb.AppendLine("}");
            sb.AppendLine("</script>");
            return sb.ToString();
        }


        private string BuildComponentChartsHtml()
        {
            var labels = new List<string>
    {
        "300um_1","300um_2","300um_3","300um_4",
        "400um_1","400um_2","400um_3","400um_4",
        "500um_1","500um_2","500um_3","500um_4",
        "600um_1","600um_2","600um_3","600um_4",
        "700um_1","700um_2","700um_3","700um_4"
    };

            List<double>[] src = new List<double>[] { CmpHeight_1_1, CmpHeight_1_2, CmpHeight_1_3, CmpHeight_1_4,CmpHeight_1_5,
            CmpHeight_2_1, CmpHeight_2_2, CmpHeight_2_3, CmpHeight_2_4,CmpHeight_2_5,
            CmpHeight_3_1, CmpHeight_3_2, CmpHeight_3_3, CmpHeight_3_4,CmpHeight_3_5,
            CmpHeight_4_1, CmpHeight_4_2, CmpHeight_4_3, CmpHeight_4_4,CmpHeight_4_5};

            var avgList = new List<double>();
            var p2pList = new List<double>();

            for (int i = 0; i < src.Length; i++)
            {
                avgList.Add(SafeAverage(src[i]));
                p2pList.Add(SafeP2P(src[i]));
            }

            string avgCanvasId = "pitchAvgChart";
            string p2pCanvasId = "pitchP2PChart";

            var sb = new StringBuilder();

            sb.AppendLine("<div class='chart-grid'>");
            sb.AppendLine("  <div class='chart-box'>");
            sb.AppendLine("    <div class='chart-title'>Component Height 平均值</div>");
            sb.AppendLine("    <canvas id='" + avgCanvasId + "' height='120'></canvas>");
            sb.AppendLine("  </div>");
            sb.AppendLine("  <div class='chart-box'>");
            sb.AppendLine("    <div class='chart-title'>Component Height P-P</div>");
            sb.AppendLine("    <canvas id='" + p2pCanvasId + "' height='120'></canvas>");
            sb.AppendLine("  </div>");
            sb.AppendLine("</div>");

            sb.AppendLine("<script>");
            sb.AppendLine("new Chart(document.getElementById('" + avgCanvasId + "'), {");
            sb.AppendLine(" type: 'bar',");
            sb.AppendLine(" data: {");
            sb.AppendLine("   labels: " + JsArrayString(labels) + ",");
            sb.AppendLine("   datasets: [{");
            sb.AppendLine("     label: 'Average',");
            sb.AppendLine("     data: " + JsArrayDouble(avgList));
            sb.AppendLine("   }]");
            sb.AppendLine(" },");
            sb.AppendLine(" options: { responsive:true, plugins:{ legend:{ display:true } }, scales:{ y:{ beginAtZero:false } } }");
            sb.AppendLine("});");

            sb.AppendLine("new Chart(document.getElementById('" + p2pCanvasId + "'), {");
            sb.AppendLine(" type: 'bar',");
            sb.AppendLine(" data: {");
            sb.AppendLine("   labels: " + JsArrayString(labels) + ",");
            sb.AppendLine("   datasets: [{");
            sb.AppendLine("     label: 'P-P',");
            sb.AppendLine("     data: " + JsArrayDouble(p2pList));
            sb.AppendLine("   }]");
            sb.AppendLine(" },");
            sb.AppendLine(" options: { responsive:true, plugins:{ legend:{ display:true } }, scales:{ y:{ beginAtZero:true } } }");
            sb.AppendLine("});");
            sb.AppendLine("</script>");

            return sb.ToString();
        }

        private string BuildComponentRawSectionHtml()
        {
            List<double>[] src = new List<double>[] { CmpHeight_1_1, CmpHeight_1_2, CmpHeight_1_3, CmpHeight_1_4,CmpHeight_1_5,
            CmpHeight_2_1, CmpHeight_2_2, CmpHeight_2_3, CmpHeight_2_4,CmpHeight_2_5,
            CmpHeight_3_1, CmpHeight_3_2, CmpHeight_3_3, CmpHeight_3_4,CmpHeight_3_5,
            CmpHeight_4_1, CmpHeight_4_2, CmpHeight_4_3, CmpHeight_4_4,CmpHeight_4_5};

            var all = new List<double>();
            for (int i = 0; i < src.Length; i++)
                if (src[i] != null) all.AddRange(src[i]);


            var sb = new StringBuilder();


            sb.AppendLine("<div style='margin-top:12px;'>");
            sb.AppendLine(BuildComponentRawHtml());
            sb.AppendLine("</div>");

            return sb.ToString();
        }

        private string BuildComponentRawHtml()
        {
            // Left / Right 的資料來源
            List<double>[] src = new List<double>[] { CmpHeight_1_1, CmpHeight_1_2, CmpHeight_1_3, CmpHeight_1_4,CmpHeight_1_5,
            CmpHeight_2_1, CmpHeight_2_2, CmpHeight_2_3, CmpHeight_2_4,CmpHeight_2_5,
            CmpHeight_3_1, CmpHeight_3_2, CmpHeight_3_3, CmpHeight_3_4,CmpHeight_3_5,
            CmpHeight_4_1, CmpHeight_4_2, CmpHeight_4_3, CmpHeight_4_4,CmpHeight_4_5};

            string[] keys = new string[]
            {
        "300um_1","300um_2","300um_3","300um_4",
        "400um_1","400um_2","400um_3","400um_4",
        "500um_1","500um_2","500um_3","500um_4",
        "600um_1","600um_2","600um_3","600um_4",
        "700um_1","700um_2","700um_3","700um_4"
            };

            var sb = new StringBuilder();

            for (int t = 0; t < InspectTimes; t++)
            {
                sb.AppendLine("<details open>");
                sb.AppendLine("  <summary>Run #" + (t + 1) + "</summary>");
                sb.AppendLine("  <table class='table'>");
                sb.AppendLine("    <thead><tr><th>Index</th><th class='num'>Value</th></tr></thead>");
                sb.AppendLine("    <tbody>");

                for (int k = 0; k < keys.Length; k++)
                {
                    string valueText = "-";
                    if (src[k] != null && src[k].Count > t)
                        valueText = HtmlUtil.F3(src[k][t]);

                    sb.AppendLine("      <tr><td>" + HtmlUtil.H(keys[k]) + "</td><td class='num'>" + valueText + "</td></tr>");
                }

                sb.AppendLine("    </tbody>");
                sb.AppendLine("  </table>");
                sb.AppendLine("</details>");
            }

            return sb.ToString();
        }

        private string BuildComponentAccuracySectionHtml()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<table class='table'>");
            sb.AppendLine("  <thead>");
            sb.AppendLine("    <tr>");
            sb.AppendLine("      <th>CRITERION</th>");
            sb.AppendLine("      <th class='num'>Component P - P</th>");
            sb.AppendLine("      <th class='num'>Component STD</th>");
            sb.AppendLine("      <th class='num'>SPECIFIED</th>");
            sb.AppendLine("      <th class='num'>STATUS</th>");
            sb.AppendLine("    </tr>");
            sb.AppendLine("  </thead>");
            sb.AppendLine("  <tbody>");

            sb.AppendLine(CalcComponentRowHtml(CmpHeight_1_1, "300um_1"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_2_1, "300um_2"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_3_1, "300um_3"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_4_1, "300um_4"));
            sb.AppendLine(CalcComponentRowHtml(CmpHeight_1_2, "400um_1"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_2_2, "400um_2"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_3_2, "400um_3"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_4_2, "400um_4"));
            sb.AppendLine(CalcComponentRowHtml(CmpHeight_1_3, "500um_1"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_2_3, "500um_2"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_3_3, "500um_3"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_4_3, "500um_4"));
            sb.AppendLine(CalcComponentRowHtml(CmpHeight_1_4, "600um_1"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_2_4, "600um_2"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_3_4, "600um_3"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_4_4, "600um_4"));
            sb.AppendLine(CalcComponentRowHtml(CmpHeight_1_5, "700um_1"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_2_5, "700um_2"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_3_5, "700um_3"));
            sb.AppendLine(CalcCirclePitchRowHtml(CmpHeight_4_5, "700um_4"));


            sb.AppendLine("  </tbody>");
            sb.AppendLine("</table>");

            return sb.ToString();
        }

        private string CalcComponentRowHtml(List<double> rawData, string dataName)
        {
            // 避免 rawData 空的時候 Max/Min 會爆
            if (rawData == null || rawData.Count == 0)
            {
                return "<tr><td>" + HtmlUtil.H(dataName) + "</td><td class='num'>-</td><td class='num'>-</td><td class='num'>-</td><td class='num'>-</td></tr>";
            }

            double stdValue = GetStandardDeviation(rawData);
            double p2p = GetMaxSubMin(rawData.Max(), rawData.Min());

            double specValue = 0.0087;
            double limit = MM2Unit(specValue);

            bool fail = p2p > limit;
            string status = fail ? "<span class='badge-ng'>FAIL</span>" : "<span class='badge-ok'>PASS</span>";

            var sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<td>" + HtmlUtil.H(dataName) + "</td>");
            sb.Append("<td class='num'>" + HtmlUtil.F3(p2p) + "</td>");
            sb.Append("<td class='num'>" + HtmlUtil.F3(stdValue) + "</td>");
            sb.Append("<td class='num'>&lt;" + HtmlUtil.F3(limit) + "</td>");
            sb.Append("<td class='num'>" + status + "</td>");
            sb.Append("</tr>");

            return sb.ToString();
        }

    }


    public static class HtmlUtil
    {
        public static string H(string s)
        {
            return WebUtility.HtmlEncode(s ?? "");
        }

        public static string F3(double v)
        {
            return v.ToString("0.000", CultureInfo.InvariantCulture);
        }

        public static string Css()
        {
            var sb = new StringBuilder();

            sb.AppendLine("<style>");
            sb.AppendLine("body{");
            sb.AppendLine("    margin:0;");
            sb.AppendLine("    background:#ffffff;");
            sb.AppendLine("    color:#000000;");
            sb.AppendLine("    font-family:Segoe UI, Arial, Microsoft JhengHei;");
            sb.AppendLine("    font-size:14px;");
            sb.AppendLine("}");

            sb.AppendLine(".container{");
            sb.AppendLine("    max-width:1100px;");
            sb.AppendLine("    margin:30px auto;");
            sb.AppendLine("    padding:0 20px;");
            sb.AppendLine("}");

            sb.AppendLine(".top{");
            sb.AppendLine("    display:flex;");
            sb.AppendLine("    justify-content:space-between;");
            sb.AppendLine("    align-items:flex-end;");
            sb.AppendLine("    margin-bottom:20px;");
            sb.AppendLine("}");

            sb.AppendLine("h1{");
            sb.AppendLine("    margin:0;");
            sb.AppendLine("    font-size:24px;");
            sb.AppendLine("}");

            sb.AppendLine(".sub{");
            sb.AppendLine("    color:#333;");
            sb.AppendLine("    font-size:13px;");
            sb.AppendLine("}");

            sb.AppendLine(".pill{");
            sb.AppendLine("    border:1px solid #aaa;");
            sb.AppendLine("    padding:6px 10px;");
            sb.AppendLine("    border-radius:6px;");
            sb.AppendLine("    background:#f3f3f3;");
            sb.AppendLine("}");

            sb.AppendLine(".card{");
            sb.AppendLine("    border:1px solid #ccc;");
            sb.AppendLine("    border-radius:8px;");
            sb.AppendLine("    margin-top:15px;");
            sb.AppendLine("}");

            sb.AppendLine(".card-h{");
            sb.AppendLine("    background:#f5f5f5;");
            sb.AppendLine("    padding:10px;");
            sb.AppendLine("    border-bottom:1px solid #ccc;");
            sb.AppendLine("    display:flex;");
            sb.AppendLine("    justify-content:space-between;");
            sb.AppendLine("}");

            sb.AppendLine(".h2{");
            sb.AppendLine("    font-weight:bold;");
            sb.AppendLine("    font-size:16px;");
            sb.AppendLine("}");

            sb.AppendLine(".body{");
            sb.AppendLine("    padding:10px;");
            sb.AppendLine("}");

            sb.AppendLine(".kpi{");
            sb.AppendLine("    display:flex;");
            sb.AppendLine("    gap:10px;");
            sb.AppendLine("}");

            sb.AppendLine(".k{");
            sb.AppendLine("    border:1px solid #ccc;");
            sb.AppendLine("    padding:10px;");
            sb.AppendLine("    width:120px;");
            sb.AppendLine("    background:#fafafa;");
            sb.AppendLine("}");

            sb.AppendLine(".kl{");
            sb.AppendLine("    font-size:12px;");
            sb.AppendLine("    color:#666;");
            sb.AppendLine("}");

            sb.AppendLine(".kv{");
            sb.AppendLine("    font-size:18px;");
            sb.AppendLine("    margin-top:4px;");
            sb.AppendLine("}");

            sb.AppendLine(".table{");
            sb.AppendLine("    width:100%;");
            sb.AppendLine("    border-collapse:collapse;");
            sb.AppendLine("}");

            sb.AppendLine(".table th{");
            sb.AppendLine("    background:#efefef;");
            sb.AppendLine("    border:1px solid #ccc;");
            sb.AppendLine("    padding:8px;");
            sb.AppendLine("    text-align:left;");
            sb.AppendLine("}");

            sb.AppendLine(".table td{");
            sb.AppendLine("    border:1px solid #ccc;");
            sb.AppendLine("    padding:8px;");
            sb.AppendLine("}");

            sb.AppendLine(".num{");
            sb.AppendLine("    text-align:right;");
            sb.AppendLine("}");

            sb.AppendLine(".badge-ok{");
            sb.AppendLine("    color:green;");
            sb.AppendLine("    font-weight:bold;");
            sb.AppendLine("}");

            sb.AppendLine(".badge-ng{");
            sb.AppendLine("    color:red;");
            sb.AppendLine("    font-weight:bold;");
            sb.AppendLine("}");

            sb.AppendLine("details{");
            sb.AppendLine("    margin-top:10px;");
            sb.AppendLine("    border:1px solid #ccc;");
            sb.AppendLine("}");

            sb.AppendLine("summary{");
            sb.AppendLine("    padding:8px;");
            sb.AppendLine("    background:#f0f0f0;");
            sb.AppendLine("    cursor:pointer;");
            sb.AppendLine("}");

            sb.AppendLine(".chart-grid{display:grid;grid-template-columns:1fr 1fr;gap:16px;margin-top:12px;}");
            sb.AppendLine(".chart-box{border:1px solid #ccc;border-radius:8px;padding:12px;background:#fff;}");
            sb.AppendLine(".chart-title{font-size:15px;font-weight:bold;margin-bottom:8px;}");

            sb.AppendLine(".tabs{margin-top:20px;}");
            sb.AppendLine(".tab-buttons{display:flex;flex-wrap:wrap;border-bottom:1px solid #ccc;margin-bottom:15px;}");
            sb.AppendLine(".tab-btn{padding:10px 16px;cursor:pointer;border:1px solid #ccc;border-bottom:none;background:#f5f5f5;margin-right:4px;border-top-left-radius:6px;border-top-right-radius:6px;}");
            sb.AppendLine(".tab-btn.active{background:#ffffff;font-weight:bold;}");
            sb.AppendLine(".tab-panel{display:none;}");
            sb.AppendLine(".tab-panel.active{display:block;}");

            sb.AppendLine(".tab-btn{padding:10px 16px;cursor:pointer;border:1px solid #ccc;border-bottom:none;background:#f0f0f0;margin-right:4px;border-top-left-radius:6px;border-top-right-radius:6px;color:#333;}");
            sb.AppendLine(".tab-btn:hover{background:#e8e8e8;}");
            sb.AppendLine(".tab-btn.active{background:#ffffff;font-weight:bold;position:relative;top:1px;}");

            sb.AppendLine("</style>");

            return sb.ToString();
        }
    }
}
