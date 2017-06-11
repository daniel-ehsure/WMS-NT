using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Data.OleDb;
using System.Data;
using System.IO;
using Model;
/*******************
 * 2008.06.27
 *******************/
namespace Util
{
    //该类是整个应用程序的帮助类
    public class Utility
    {
        /// <summary>
        /// 初始化窗体大小 和标题
        /// </summary>
        public static void init(Form form)
        {
            form.Height = Global.baseHeight;
            form.Width = Global.baseWidth;
        }
        /// <summary>
        /// 保存配置文件参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void writeConfig(string key, string value)
        {
            XmlDocument doc = new XmlDocument();

            //获得配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "刀具库管理系统.exe.config";

            doc.Load(strFileName);

            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");

            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素              
                if (att.Value == key)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["value"];
                    att.Value = value;
                    break;
                }
            }

            //保存上面的修改
            doc.Save(strFileName);

        }


        /// <summary>
        /// 返回以0开头的id编号
        /// </summary>
        /// <param name="id">数字id号</param>
        /// <param name="zero">零的个数</param>
        /// <returns></returns>
        public static string setID(long id, int zero)
        {
            string zeros = string.Empty;
            string temp = id.ToString();
            int length = zero - temp.Length + 1;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    zeros += "0";
                }
            }
            else if (length == 0)
            {
                return temp;
            }
            else
            {
                return string.Empty;
            }
            return zeros + id;
        }
        /// <summary>
        /// 将DataGridView中的数据导出到Excel中
        /// </summary>
        /// <param name="gridView">DataGridView</param>
        /// <param name="fileName">目标文件名</param>
        /// <param name="isShowExcle">是否显示Excel文件</param>
        /// <returns></returns>
        public static bool exportExcel(DataGridView gridView, string fileName, bool isShowExcle)
        {


            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (app == null)
                {
                    return false;
                }
                app.Visible = isShowExcle;
                Workbooks workbooks = app.Workbooks;
                _Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Sheets sheets = workbook.Worksheets;

                _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                worksheet.Cells.Columns.NumberFormat = "@";
                if (worksheet == null)
                {
                    return false;
                }
                string sLen = "";
                //取得最后一列列名
                char H = (char)(64 + gridView.ColumnCount / 26);
                char L = (char)(64 + gridView.ColumnCount % 26);
                if (gridView.ColumnCount < 26)
                {
                    sLen = L.ToString();
                }
                else
                {
                    sLen = H.ToString() + L.ToString();
                }

                //标题
                string sTmp = sLen + "1";
                Range ranCaption = worksheet.get_Range(sTmp, "A1");
                string[] asCaption = new string[gridView.ColumnCount];
                for (int i = 0; i < gridView.ColumnCount; i++)
                {
                    asCaption[i] = gridView.Columns[i].HeaderText;
                }
                ranCaption.Value2 = asCaption;

                //数据
                for (int r = 0; r < gridView.RowCount; r++)
                {
                    for (int l = 0; l < gridView.Columns.Count; l++)
                    {
                        if (gridView[l, r].Value != null)
                        {

                            worksheet.Cells[r + 2, l + 1] = gridView[l, r].Value.ToString().Trim();
                        }
                    }
                }


                workbook.SaveCopyAs(fileName);
                workbook.Saved = true;
                //   workbook.Close(false, true, null);

            }
            catch
            {
                return false;
            }
            finally
            {
                //关闭
                // app.UserControl = false;
                // app.Quit();
                //  KillExcel();
            }
            return true;

        }

        /// <summary>
        /// 将DataGridView中的数据导出到Excel中
        /// </summary>
        /// <param name="gridView">DataGridView</param>
        /// <param name="fileName">目标文件名</param>
        /// <param name="isShowExcle">是否显示Excel文件</param>
        /// <returns></returns>
        public static bool exportExcel2(DataGridView gridView, string fileName, bool isShowExcle)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                if (app == null)
                {
                    return false;
                }

                app.Visible = isShowExcle;

                Workbooks workbooks = app.Workbooks;

                _Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                Sheets sheets = workbook.Worksheets;

                _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);

                worksheet.Cells.Columns.NumberFormat = "@";

                if (worksheet == null)
                {
                    return false;
                }

                string sLen = "";

                //总可见列数，总可见行数
                int colCount = gridView.Columns.GetColumnCount(DataGridViewElementStates.Visible);
                int rowCount = gridView.Rows.GetRowCount(DataGridViewElementStates.Visible);


                //取得最后一列列名
                //char H = (char)(64 + gridView.ColumnCount / 26);

                //char L = (char)(64 + gridView.ColumnCount % 26);

                char H = (char)(64 + colCount / 26);

                char L = (char)(64 + colCount % 26);



                if (colCount < 26)
                {
                    sLen = L.ToString();
                }
                else
                {
                    sLen = H.ToString() + L.ToString();
                }

                //标题
                string sTmp = sLen + "1";

                Range ranCaption = worksheet.get_Range(sTmp, "A1");

                //string[] asCaption = new string[gridView.ColumnCount];
                string[] asCaption = new string[colCount];

                //for (int i = 0; i < gridView.ColumnCount; i++)
                //{
                //    if (gridView.Columns[i].Visible)
                //    {
                //        asCaption[i] = gridView.Columns[i].HeaderText;
                //    }
                //} 


                for (int i = 0; i < colCount; i++)
                {
                    if (gridView.Columns[i].Visible)
                    {

                        asCaption[i] = gridView.Columns[i].HeaderText;
                    }
                }

                ranCaption.Value2 = asCaption;

                //数据
                //for (int r = 0; r < gridView.RowCount; r++)
                //{
                //    for (int l = 0; l < gridView.Columns.Count; l++)
                //    {
                //        if (gridView[l, r].Value != null)
                //        {
                //            if (gridView.Columns[l].Visible)
                //            {
                //                worksheet.Cells[r + 2, l + 1] = gridView[l, r].Value.ToString().Trim();
                //            }
                //        }
                //    }
                //}

                for (int r = 0; r < rowCount; r++)
                {
                    for (int l = 0; l < colCount; l++)
                    {
                        if (gridView[l, r].Value != null)
                        {
                            if (gridView.Columns[l].Visible)
                            {
                                worksheet.Cells[r + 2, l + 1] = gridView[l, r].Value.ToString().Trim();
                            }
                        }
                    }
                }


                workbook.SaveCopyAs(fileName);

                workbook.Saved = true;
                //   workbook.Close(false, true, null);

            }
            catch
            {
                return false;
            }
            finally
            {
                //关闭
                // app.UserControl = false;
                // app.Quit();
                //  KillExcel();
            }
            return true;
        }
        /// <summary>
        /// 将DataGridView中的数据导出到Excel中
        /// </summary>
        /// <param name="gridView">DataGridView</param>
        /// <param name="fileName">目标文件名</param>
        /// <param name="isShowExcle">是否显示Excel文件</param>
        /// <returns></returns>
        public static bool exportExcel3(DataGridView gridView, string fileName, bool isShowExcle)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                if (app == null)
                {
                    return false;
                }

                app.Visible = isShowExcle;

                Workbooks workbooks = app.Workbooks;

                _Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);

                Sheets sheets = workbook.Worksheets;

                _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);

                worksheet.Cells.Columns.NumberFormat = "@";

                if (worksheet == null)
                {
                    return false;
                }

                string sLen = "";

                //总可见列数，总可见行数
                int colCount = gridView.Columns.GetColumnCount(DataGridViewElementStates.Visible) + 1;
                int rowCount = gridView.Rows.GetRowCount(DataGridViewElementStates.Visible);
                //总列数
                int colCountAll = gridView.Columns.Count ;


                //取得最后一列列名
                //char H = (char)(64 + gridView.ColumnCount / 26);

                //char L = (char)(64 + gridView.ColumnCount % 26);

                char H = (char)(64 + colCount / 26);

                char L = (char)(64 + colCount % 26);



                if (colCount < 26)
                {
                    sLen = L.ToString();
                }
                else
                {
                    sLen = H.ToString() + L.ToString();
                }

                //标题
                string sTmp = sLen + "1";

                Range ranCaption = worksheet.get_Range(sTmp, "A1");

                //string[] asCaption = new string[gridView.ColumnCount];
                string[] asCaption = new string[colCount];

                //for (int i = 0; i < gridView.ColumnCount; i++)
                //{
                //    if (gridView.Columns[i].Visible)
                //    {
                //        asCaption[i] = gridView.Columns[i].HeaderText;
                //    }
                //} 

                int j = 0;
                for (int i = 0; i < colCountAll; i++)
                {

                    if ((i==0) || (gridView.Columns[i].Visible == true))
                    {
                        asCaption[j] = gridView.Columns[i].HeaderText;
                        j++;
                    }
                }

                ranCaption.Value2 = asCaption;

                //数据
                //for (int r = 0; r < gridView.RowCount; r++)
                //{
                //    for (int l = 0; l < gridView.Columns.Count; l++)
                //    {
                //        if (gridView[l, r].Value != null)
                //        {
                //            if (gridView.Columns[l].Visible)
                //            {
                //                worksheet.Cells[r + 2, l + 1] = gridView[l, r].Value.ToString().Trim();
                //            }
                //        }
                //    }
                //}

                
                for (int r = 0; r < rowCount; r++)
                {
                    int k = 0;
                    for (int l = 0; l < colCountAll; l++)
                    {
                        if ((l==0) || (gridView.Columns[l].Visible == true))
                        {
                            if (gridView[l, r].Value != null)
                            {

                                if (l == 0)
                                {
                                    if (gridView[0, r].Value.ToString() == "1")
                                    {
                                        worksheet.Cells[r + 2, k + 1] = "工装";
                                    }
                                    else if (gridView[0, r].Value.ToString() == "2")
                                    {

                                        worksheet.Cells[r + 2, k + 1] = "附件";
                                    }
                                    else
                                    {
                                        worksheet.Cells[r + 2, k + 1] = gridView[l, r].Value.ToString().Trim();
                                    }
                                }
                                else
                                {
                                    worksheet.Cells[r + 2, k + 1] = gridView[l, r].Value.ToString().Trim();
                                }
                            }
                            k++;
                        }
                    }
                }


                workbook.SaveCopyAs(fileName);

                workbook.Saved = true;
                //   workbook.Close(false, true, null);

            }
            catch
            {
                return false;
            }
            finally
            {
                //关闭
                // app.UserControl = false;
                // app.Quit();
                //  KillExcel();
            }
            return true;
        }

        /// <summary>
        /// 关闭Excel进程
        /// </summary>
        private static void KillExcel()
        {
            try
            {
                Process[] processes = System.Diagnostics.Process.GetProcessesByName("EXCEL");
                foreach (Process process in processes)
                {
                    if (process.ProcessName.ToLower().Equals("excel "))
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(("ERROR   " + exception.Message));
            }
        }
        /// <summary>
        /// 从Excel表中读取数据
        /// </summary>
        /// <param name="Path">文件路径</param>        
        /// <returns></returns>
        public static DataSet ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            string tableName = schemaTable.Rows[0][2].ToString().Trim();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [" + tableName + "]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            conn.Close();
            return ds;
        }
        /// <summary>
        /// 通过数据表的名称获得在与其关联的数据表中是否引用的sql语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> getReflectTable(string tableName)
        {
            List<string> list = new List<string>();
            string sql = string.Empty;
            switch (tableName.ToLower())
            {
                case "t_dm_adjust": sql = "SELECT count(*)  FROM T_OPERATE_CHECK_ADJUST WHERE T_OPERATE_CHECK_ADJUST.C_MENO = @id";
                    list.Add(sql);
                    break;
                case "t_dm_caizhi": sql = "SELECT count(*)  FROM t_operate_stocks WHERE  t_operate_stocks.c_unit = @id";
                    list.Add(sql);
                    break;
                case "t_dm_gangwei": sql = "SELECT count(*) FROM T_JB_EMPLOYEE  WHERE T_JB_EMPLOYEE.C_GANGWEI = @id";
                    list.Add(sql);
                    break;
                case "t_dm_materiel_pinpai": sql = "SELECT count(*) FROM T_OPERATE_INOUT_SUB  WHERE T_OPERATE_INOUT_SUB.C_IN_UNIT = @id";
                    list.Add(sql);
                    sql = "SELECT count(*)  FROM T_OPERATE_STOCKS WHERE T_OPERATE_STOCKS.C_IN_UNIT = @id	";
                    list.Add(sql);
                    sql = "SELECT count(*)  FROM T_OPERATE_STOCKS_DUST WHERE T_OPERATE_STOCKS_DUST.C_IN_UNIT = @id";
                    list.Add(sql);
                    break;
                case "t_dm_materiel_unit": sql = "SELECT count(*)  FROM T_JB_MATERIEL  WHERE T_JB_MATERIEL.C_UNIT_ID = @id";
                    list.Add(sql);
                    break;
                case "t_dm_object": sql = "SELECT count(*) FROM T_JB_MATERIEL  WHERE T_JB_MATERIEL.C_OBJECT = @id";
                    list.Add(sql);
                    break;
                case "t_dm_place": sql = "SELECT count(*) FROM T_JB_MATERIEL  WHERE T_JB_MATERIEL.C_PLACE = @id";
                    list.Add(sql);
                    sql = "SELECT count(*)  FROM T_JB_PLACE  WHERE T_JB_PLACE.C_PLACE = @id";//INTO :ll_count
                    list.Add(sql);
                    break;
                case "t_jb_cc": sql = "SELECT count(*) FROM T_JB_PLACE  	WHERE substring(T_JB_PLACE.c_id,1,2) =@id and len(T_JB_PLACE.c_id)>2";
                    list.Add(sql);
                    break;
                case "t_dm_unit": sql = "SELECT count(*) FROM T_JB_EMPLOYEE WHERE T_JB_EMPLOYEE.C_UNIT_ID = @id";
                    list.Add(sql);
                    break;
                case "t_jb_employee": sql = "SELECT count(*) FROM T_OPERATE_INOUT_MAIN  WHERE T_OPERATE_INOUT_MAIN.C_PEOPLE_ID =@id";
                    list.Add(sql);
                    break;
                case "t_dm_planetype": sql = "SELECT count(*) FROM T_JB_MATERIEL a,t_dm_planetype c  WHERE c.c_id =@id and a.c_planetype=c.c_name";
                    list.Add(sql);
                    break;
            }
            return list;
        }

        public static string getPicName(int i)
        {
            string name = ".bmp";
            switch (i)
            {
                case 0: name = ".bmp"; break;
                case 1: name = ".jpg"; break;
                case 2: name = ".jpeg"; break;
                case 3: name = ".gif"; break;
            }
            return name;
        }
        /// <summary>
        /// 将数据网格的数据导出到Excel文件中,该文件不能被导入
        /// 该方法比exportExcel快
        /// </summary>
        /// <param name="dataGridView1"></param>
        public static bool saveasexcle(DataGridView dataGridView1)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl   files   (*.xls) |*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = true;
            saveFileDialog.Title = "导出Excel文件到 ";

            //saveFileDialog.ShowDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream myStream;
                myStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding("GB2312"));
                string str = " ";
                try
                {
                    //写标题    
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        if (i > 0)
                        {
                            str += "\t ";
                        }
                        str += dataGridView1.Columns[i].HeaderText;
                    }

                    sw.WriteLine(str);
                    //写内容  
                    for (int j = 0; j <= dataGridView1.RowCount - 1; j++)
                    {
                        string tempStr = " ";
                        for (int k = 0; k < dataGridView1.Columns.Count; k++)
                        {
                            if (k > 0)
                            {
                                tempStr += "\t ";
                            }
                            tempStr += dataGridView1.Rows[j].Cells[k].Value.ToString();
                        }
                        sw.WriteLine(tempStr);
                    }
                    sw.Close();
                    myStream.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    sw.Close();
                    myStream.Close();
                }

            }
            else
            {
                return false;

            }
        }



        /// <summary>
        /// 将DataGridView中的数据导出到Excel中
        /// </summary>
        /// <param name="gridView">DataGridView</param>
        /// <param name="fileName">目标文件名</param>
        /// <param name="isShowExcle">是否显示Excel文件</param>
        /// <returns></returns>
        public static bool exportExcel(DataGridView gridView, string fileName, bool isShowExcle, int columncount)
        {


            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                if (app == null)
                {
                    return false;
                }
                app.Visible = isShowExcle;
                Workbooks workbooks = app.Workbooks;
                _Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Sheets sheets = workbook.Worksheets;
                _Worksheet worksheet = (_Worksheet)sheets.get_Item(1);
                worksheet.Cells.Columns.NumberFormat = "@";
                if (worksheet == null)
                {
                    return false;
                }
                string sLen = "";
                //取得最后一列列名
                char H = (char)(64 + gridView.ColumnCount / 26);
                char L = (char)(64 + gridView.ColumnCount % 26);
                if (gridView.ColumnCount < 26)
                {
                    sLen = L.ToString();
                }
                else
                {
                    sLen = H.ToString() + L.ToString();
                }

                //标题
                string sTmp = sLen + "1";
                Range ranCaption = worksheet.get_Range(sTmp, "A1");
                string[] asCaption = new string[gridView.ColumnCount];
                for (int i = 0; i < columncount; i++)
                {
                    asCaption[i] = gridView.Columns[i].HeaderText;
                }
                ranCaption.Value2 = asCaption;

                //数据
                for (int r = 0; r < gridView.RowCount; r++)
                {
                    for (int l = 0; l < columncount; l++)
                    {
                        if (gridView[l, r].Value != null)
                        {
                            worksheet.Cells[r + 2, l + 1] = gridView[l, r].Value.ToString().Trim();
                        }
                    }
                }


                workbook.SaveCopyAs(fileName);
                workbook.Saved = true;
                //   workbook.Close(false, true, null);

            }
            catch
            {
                return false;
            }
            finally
            {
                //关闭
                // app.UserControl = false;
                // app.Quit();
                //  KillExcel();
            }
            return true;

        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
                return false;
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 进行加解密,目前支持到16位二进制数
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>

        public static string encrypt(string encrypt)
        {
            string strOld = "", strNew = "", strOther = "";
            int intstrlen = 0;
            Int64 bitbyte1;
            strOld = encrypt;
            char[] cr = strOld.ToCharArray(0, strOld.Length);
            intstrlen = strOld.Length;

            for (int len = 0; len < intstrlen; len++)
            {
                bitbyte1 = Convert.ToInt64(cr[len]);
                strOther = f_binary(bitbyte1.ToString(), 0);
                strOther = strResever(strOther);
                strOther = f_binary(strOther, 1);
                strNew += "|" + Convert.ToChar(Convert.ToInt64(strOther)) + "|";
            }
            return strNew;
        }
        //解密
        public static string decrypt(string decrypt)
        {
            string strOld = "", strNew = "", strOther = "";
            int intstrlen = 0;
            strOld = decrypt;
            intstrlen = strOld.Length;
            char[] creturn = strOld.ToCharArray(0, strOld.Length);
            Int64 bitbyte1 = 0;
            for (int len = 0; len < intstrlen; len++)
            {
                if (creturn[len] == '|')
                    continue;
                bitbyte1 = Convert.ToInt64(Convert.ToChar(creturn[len]));
                strOther = f_binary(bitbyte1.ToString(), 0);
                strOther = strResever(strOther);
                strOther = f_binary(strOther, 1);
                strNew += Convert.ToChar(Convert.ToInt64(strOther));
            }

            return strNew;
        }

        public static string f_binary(string bit, int flag)
        {
            Int64 newbit = 0, intzero = 0; ;
            string strBin = "", strReturn = "";

            if (flag == 0)//转二进制
            {
                newbit = Convert.ToInt64(bit);
                while (newbit / 2 >= 1)
                {
                    strBin += Convert.ToString(newbit % 2);
                    newbit /= 2;
                }
                strBin += Convert.ToString(newbit % 2);
                intzero = 16 - strBin.Length;
                if (intzero > 0)
                {
                    for (int i = 0; i < intzero; i++)
                    {
                        strReturn += '0';
                    }
                }
                strBin = strBin + strReturn;
                strReturn = strBin;
                strReturn = strResever(strReturn);
                return strReturn;
            }
            else//转十进制
            {

                int lenstr = 0, j = 0;
                lenstr = bit.Length;
                // strBin = bit;
                strBin = strResever(bit);
                for (int i = 0; i < lenstr; i++)
                {
                    if (strBin.Substring(i, 1) == "1")
                    {
                        j += Convert.ToInt32(Math.Pow(2.0, Convert.ToDouble(i)));
                    }
                }
                strReturn = j.ToString();
            }
            return strReturn;
        }
        //取反
        public static string strResever(string str)
        {
            string asStr = "";
            int len = str.Length;
            for (int i = len - 1; i >= 0; i--)
            {
                asStr += str[i];
            }
            return asStr;
        }


        /// <summary>
        /// 保存配置文件参数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void writeConfig2(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            //获得配置文件的全路径
            string strFileName = AppDomain.CurrentDomain.BaseDirectory.ToString() + "沈飞物流管控一体化软件.exe.config";
            doc.Load(strFileName);
            //找出名称为“add”的所有元素
            XmlNodeList nodes = doc.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素    
                if (att != null)
                {
                    continue;
                }
                else
                {
                    att = nodes[i].Attributes["name"];
                }
                if (att.Value == key)
                {
                    //对目标元素中的第二个属性赋值
                    att = nodes[i].Attributes["connectionString"];
                    att.Value = value;
                    break;
                }
            }
            //保存上面的修改
            doc.Save(strFileName);
        }

        /// <summary>
        /// 解密连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string getConnectionString(string connectionString)
        {
            int index = connectionString.LastIndexOf("=") + 1;
            string temp = connectionString.Substring(index + 1, connectionString.Length - index - 1);
            connectionString = connectionString.Substring(0, index + 1);
            temp = Utility.decrypt(temp);
            connectionString += temp;
            return connectionString;
        }

        /// <summary> 字符串替换
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ReplaceString(string source)
        {
            //return source.Replace(",", "，").Replace("'", "‘").Replace(";", "；").Replace(":", "：").Replace("=", "＝");
            return source.Replace(",", "，").Replace("'", "‘").Replace("+", "＋").Replace(";", "；").Replace("=", "＝").Replace(":", "：").Replace("/", "／");
        }

        public static Boolean MessLength(string text1, int nlength)
        {
            int slen = GetBytesCount(text1);

            if (slen > nlength)
            {
                //Msgboxx(sMess + "信息的长度不能大于" + nlength + "！   ", "");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int GetBytesCount(string str)
        {
            UnicodeEncoding Unicode = new UnicodeEncoding();
            int nCount = 0;

            for (int i = 0; i < str.Length; i++)
            {
                byte[] b = Unicode.GetBytes(str[i].ToString());

                if (b[1] == 0)
                    nCount++;
                else
                    nCount += 2;
            }

            return nCount;
        }

        public static T_JB_COMPONENT AnalyzeBarcode(string barcode)
        {
            T_JB_COMPONENT comp = null;
            if (!(string.IsNullOrEmpty(barcode)))
            {
                string[] temps = barcode.Split("|".ToCharArray());
                if (temps.Length > 12)
                {
                    comp = new T_JB_COMPONENT();
                    List<T_JB_COMPONENT_PROCEDURE> comprs = new List<T_JB_COMPONENT_PROCEDURE>();
                    for (int i = 0; i < temps.Length; i++)
                    {
                        string[] temps1 = temps[i].Split("：".ToCharArray());
                        if (temps1.Length > 1)
                        {
                            string name = temps1[0];
                            string value = temps1[1];
                            if (i <= 11)
                            {
                                if (name.IndexOf("客户名称") >= 0)
                                {
                                    comp.c_customer_name = value;
                                }
                                else if (name.IndexOf("设计") >= 0)
                                {
                                    comp.c_designer = value;
                                }
                                else if (name.IndexOf("日期") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.d_date = DateTime.Now;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.d_date = Convert.ToDateTime(value);
                                        }
                                        catch (Exception)
                                        {
                                             comp.d_date = DateTime.Now;
                                        }
                                    }
                                }
                                else if (name.IndexOf("物料编号") >= 0)
                                {
                                    comp.c_materiel_code = value;
                                }
                                else if (name.IndexOf("零件图号") >= 0)
                                {
                                    comp.c_id = value;
                                }
                                else if (name.IndexOf("零件名称") >= 0)
                                {
                                    comp.c_name = value;
                                }
                                else if (name.IndexOf("产品名称") >= 0)
                                {
                                    comp.c_product_name = value;
                                }
                                else if (name.IndexOf("数量") >= 0)
                                {
                                    int outvalue = 0;
                                    if (string.IsNullOrEmpty(value) || Int32.TryParse(value, out outvalue) == false)
                                    {
                                        comp.i_count = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.i_count = Convert.ToInt32(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.i_count = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("面积") >= 0)
                                {                                    
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.D_acreage = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.D_acreage = Convert.ToDouble(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.D_acreage = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("厚度") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.i_thick = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.i_thick = Convert.ToDouble(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.i_thick = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("版本号") >= 0)
                                {
                                    comp.c_version = value;
                                }
                                else if (name.IndexOf("材料") >= 0)
                                {
                                    comp.c_makings = value;
                                }
                            }
                            else
                            {
                                T_JB_COMPONENT_PROCEDURE compr = new T_JB_COMPONENT_PROCEDURE();
                                compr.C_COMPONENT_ID = comp.c_id;
                                compr.C_PROCEDURE_ID = name;
                                int outvalue = 0;
                                if (string.IsNullOrEmpty(value) || Int32.TryParse(value, out outvalue) == false)
                                {
                                    compr.I_VALUE = 0;
                                }
                                else
                                {
                                    try
                                    {
                                        compr.I_VALUE = Convert.ToInt32(value);
                                    }
                                    catch (Exception)
                                    {
                                        compr.I_VALUE = 0;
                                    }
                                }
                                comprs.Add(compr);
                            }
                        }

                    }
                    comp.Procedures = comprs;
                }
            }
            return comp;
        }

        public static string getCodeBarcode(string barcode)
        {
          string tuhao = string.Empty;
            if (!(string.IsNullOrEmpty(barcode)))
            {
                string[] temps = barcode.Split("|".ToCharArray());
                if (temps.Length > 12)
                {
                    
                    for (int i = 0; i < temps.Length; i++)
                    {
                        string[] temps1 = temps[i].Split("：".ToCharArray());
                        if (temps1.Length > 1)
                        {
                            string name = temps1[0];
                            string value = temps1[1];
                            if (name.IndexOf("零件图号") >= 0)
                            {
                                tuhao = value;
                                break;
                            }
                        }

                    }
                   
                }
            }
            return tuhao;
        }

        /// <summary>
        /// 最新的物料二维码解析
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static T_JB_COMPONENT AnalyzeBarcodeNew(string barcode)
        {
            T_JB_COMPONENT comp = null;
            if (!(string.IsNullOrEmpty(barcode)))
            {
                string splitChar = "☉";
                barcode = barcode.Replace("XX", "");
                barcode = barcode.Replace("\\P", splitChar);
                string[] temps = barcode.Split(splitChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (temps.Length > 12)
                {
                    comp = new T_JB_COMPONENT();
                    List<T_JB_COMPONENT_PROCEDURE> comprs = new List<T_JB_COMPONENT_PROCEDURE>();
                    int indexStart = -1;
                    int indexEnd = -1;
                    string path = string.Empty;
                    for (int i = 0; i < temps.Length; i++)
                    {
                        if (indexStart == -1)
                        {
                            string[] temps1 = temps[i].Split("：".ToCharArray());
                            if (temps1.Length > 1)
                            {
                                string name = temps1[0];
                                string value = temps1[1];
                                int index1 = value.IndexOf("（");
                                int index2 = value.IndexOf("）");
                                if (index1 > -1 && index2 > -1)
                                {
                                    string tempvalue = value.Substring(0, index1);
                                    string tempvalue2 = value.Substring(index2 + 1, value.Length - index2 - 1);
                                    value = tempvalue + tempvalue2;
                                }
                                if (name.IndexOf("客户名称") >= 0)
                                {
                                    comp.c_customer_name = value;
                                }
                                else if (name.IndexOf("设计") >= 0)
                                {
                                    comp.c_designer = value;
                                }
                                else if (name.IndexOf("日期") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.d_date = DateTime.Now;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.d_date = Convert.ToDateTime(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.d_date = DateTime.Now;
                                        }
                                    }
                                }
                                else if (name.IndexOf("物料编号") >= 0)
                                {
                                    comp.c_materiel_code = value;
                                }
                                else if (name.IndexOf("零件图号") >= 0)
                                {
                                    comp.c_id = value;
                                }
                                else if (name.IndexOf("零件名称") >= 0)
                                {
                                    comp.c_name = value;
                                }
                                else if (name.IndexOf("产品名称") >= 0)
                                {
                                    comp.c_product_name = value;
                                }
                                else if (name.IndexOf("数量") >= 0)
                                {
                                    int outvalue = 0;
                                    if (string.IsNullOrEmpty(value) || Int32.TryParse(value, out outvalue) == false)
                                    {
                                        comp.i_count = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.i_count = Convert.ToInt32(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.i_count = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("长度") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.D_length = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.D_length = Convert.ToDouble(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.D_length = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("宽度") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.D_width = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.D_width = Convert.ToDouble(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.D_width = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("面积") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.D_acreage = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.D_acreage = Convert.ToDouble(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.D_acreage = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("厚度") >= 0)
                                {
                                    if (string.IsNullOrEmpty(value))
                                    {
                                        comp.i_thick = 0;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            comp.i_thick = Convert.ToDouble(value);
                                        }
                                        catch (Exception)
                                        {
                                            comp.i_thick = 0;
                                        }
                                    }
                                }
                                else if (name.IndexOf("版本") >= 0)
                                {
                                    comp.c_version = value;
                                }
                                else if (name.IndexOf("图纸路径") >= 0)
                                {                                    
                                    path += value;
                                    indexStart = i;
                                }
                            }
                        }
                        else
                        {
                            if (indexEnd == -1)
                            {
                                if (temps[i].IndexOf("材料") >= 0)
                                {
                                    comp.C_path = path;
                                     string[] temps1 = temps[i].Split("：".ToCharArray());
                                     if (temps1.Length > 1)
                                     {
                                         string name = temps1[0];
                                         string value = temps1[1];
                                         int index1 = value.IndexOf("（");
                                         int index2 = value.IndexOf("）");
                                         if (index1 > -1 && index2 > -1)
                                         {
                                             value = value.Substring(0, index1);
                                          //   string tempvalue2 = value.Substring(index2+1, value.Length - index2);
                                           //  value = tempvalue + tempvalue2;
                                         }
                                         comp.c_makings = value;
                                         indexEnd = i;
                                     }
                                }
                                else
                                {
                                    path += "\\" + temps[i];
                                }
                            }
                            else
                            {
                                 string[] temps1 = temps[i].Split("：".ToCharArray());
                                 if (temps1.Length > 1)
                                 {
                                     string name = temps1[0];
                                     string value = temps1[1];
                                     int indexkk = value.IndexOf("（");
                                     if (indexkk > 0)
                                     {
                                         value = value.Substring(0, indexkk);
                                     }
                                     T_JB_COMPONENT_PROCEDURE compr = new T_JB_COMPONENT_PROCEDURE();
                                     compr.C_COMPONENT_ID = comp.c_id;
                                     compr.C_PROCEDURE_ID = name;
                                     int outvalue = 0;
                                     if (string.IsNullOrEmpty(value) || Int32.TryParse(value, out outvalue) == false)
                                     {
                                         compr.I_VALUE = 0;
                                     }
                                     else
                                     {
                                         try
                                         {
                                             compr.I_VALUE = Convert.ToInt32(value);
                                         }
                                         catch (Exception)
                                         {
                                             compr.I_VALUE = 0;
                                         }
                                     }
                                     int sequence = comprs.Count+1;
                                     compr.I_sequence = sequence;
                                     comprs.Add(compr);
                                 }
                            }
                        }
                    //    if (temps1.Length > 1)
                    //    {
                            
                    //        if (i <= 11)
                    //        {
                                
                    //        }
                    //        else
                    //        {
                    //            T_JB_COMPONENT_PROCEDURE compr = new T_JB_COMPONENT_PROCEDURE();
                    //            compr.C_COMPONENT_ID = comp.c_id;
                    //            compr.C_PROCEDURE_ID = name;
                    //            int outvalue = 0;
                    //            if (string.IsNullOrEmpty(value) || Int32.TryParse(value, out outvalue) == false)
                    //            {
                    //                compr.I_VALUE = 0;
                    //            }
                    //            else
                    //            {
                    //                try
                    //                {
                    //                    compr.I_VALUE = Convert.ToInt32(value);
                    //                }
                    //                catch (Exception)
                    //                {
                    //                    compr.I_VALUE = 0;
                    //                }
                    //            }
                    //            comprs.Add(compr);
                    //        }
                    //    }

                    }
                    comp.Procedures = comprs;
                }
            }
            return comp;
        }

        public static string getCodeBarcodeNew(string barcode)
        {
            string tuhao = string.Empty;
            if (!(string.IsNullOrEmpty(barcode)))
            {
                string splitChar = "☉";
                barcode = barcode.Replace("XX", "");
                barcode = barcode.Replace("\\P", splitChar);
                string[] temps = barcode.Split(splitChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (temps.Length > 12)
                {

                    for (int i = 0; i < temps.Length; i++)
                    {
                        string[] temps1 = temps[i].Split("：".ToCharArray());
                        if (temps1.Length > 1)
                        {
                            string name = temps1[0];
                            string value = temps1[1];
                            if (name.IndexOf("零件图号") >= 0)
                            {
                                tuhao = value;
                                break;
                            }
                        }

                    }

                }
            }
            return tuhao;
        }

    }
}
