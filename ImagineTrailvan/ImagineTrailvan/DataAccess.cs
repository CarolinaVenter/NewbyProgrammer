using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ImagineTrailvan
{
     public class DataAccess
    {
         string connString = @"Data Source=Carolien-PC;Initial Catalog=ImagineTrailvanDB;Integrated Security=True";
         SqlCommand cmd;
         SqlDataAdapter adapter;
         DataSet ds;

         public DataAccess()
         {
         }//end of public DataAccess()
         #region Dynamic GetQueries
         public DataTable getTable(string tblName)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 DataSet getds;
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getTable(string tblName)

         public DataTable getRecord(string tblName, string[] fields, ArrayList values)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 string statement = "SELECT * from ";
                 statement += tblName;
                 statement += " WHERE ";
                 for (int i = 0; i <= fields.Length - 1; i++)
                 {
                     if (i < fields.Length - 1)
                     {
                         statement += fields[i] + " LIKE '%" + values[i] + "%' AND ";
                     }//end of if (i <= fields.Length - 1)
                     else
                     {
                         statement += fields[i] + " LIKE '%" + values[i]+ "%' ";
                     }//end of else (i < fields.Length - 1)
                 }//end of for (int i = 0; i <= fields.Length - 1; i++)
                 DataSet getds;
                 DataTable result = new DataTable();
                 cmd = new SqlCommand(statement, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
         }//end of public DataTable getRecord(string tblName, string username, string[] fields, string[] values)

         public DataTable getMathRecord(string tblName, string[] fields, ArrayList values)
         {
             //This is meant for queries that have direct equal parameters or have some kind of math to perform as a parameter- take note to send math equation in variable before sending object here!
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 string statement = "SELECT * from ";
                 statement += tblName;
                 statement += " WHERE ";
                 for (int i = 0; i <= fields.Length - 1; i++)
                 {
                     if (i < fields.Length - 1)
                     {
                         statement += fields[i] + values[i] + " AND ";
                     }//end of if (i <= fields.Length - 1)
                     else
                     {
                         statement += fields[i] + values[i];
                     }//end of else (i < fields.Length - 1)
                 }//end of for (int i = 0; i <= fields.Length - 1; i++)
                 DataSet getds;
                 DataTable result = new DataTable();
                 cmd = new SqlCommand(statement, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
         }//end of public DataSet getMathRecord(string tblName, string username, string[] fields, string[] values)

         #endregion
         #region Dynamic insert
         public Boolean insertCmd(string tblName, string[] fields, ArrayList values)
         {
             Boolean success = false;
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 ds = new DataSet();
                 adapter.Fill(ds, tblName);
                 conn.Close();
                 DataRow newRow = ds.Tables[tblName].NewRow();
                 for (int i = 0; i < fields.Length; i++)
                 {
                     newRow[fields[i]] = values[i];
                 }//end of for (int i = 0; i < fields.Length - 1; i++)
                 ds.Tables[tblName].Rows.Add(newRow);
                 builder.GetInsertCommand();
                 adapter.Update(ds.Tables[tblName]);
                 success = true;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
             return success;
         }//end of public bool insertCmd(string tblName, string[] fields, ArrayList values)
         #endregion
         #region Dynamic Delete
         public Boolean removeCmd(string tblName, string ID)
         {
             Boolean success = false;
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 ds = new DataSet();
                 adapter.Fill(ds, tblName);
                 conn.Close();
                 DataRow affectedRow = null;
                 foreach (DataRow row in ds.Tables[tblName].Rows)
                 {
                     if (row[0].ToString() == ID)
                     {
                         affectedRow = row;
                     }//end of if (row[0].ToString() == ID)
                 }//end of foreach (DataRow row in ds.Tables[tblName].Rows)
                 ds.Tables[tblName].Rows[ds.Tables[tblName].Rows.IndexOf(affectedRow)].Delete();
                 builder.GetDeleteCommand();
                 adapter.Update(ds.Tables[tblName]);
                 success = true;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
             return success;
         }//end of public bool removeCmd(string tblName, string ID)
         #endregion
         #region Dynamic Update
         public Boolean updateCmd(string tblName, string ID, string[] fields, ArrayList values)
         {
             Boolean success = false;
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 ds = new DataSet();
                 adapter.Fill(ds, tblName);
                 conn.Close();
                 DataRow affectedRow = null;
                 foreach (DataRow row in ds.Tables[tblName].Rows)
                 {
                     if (row[0].ToString() == ID)
                     {
                         affectedRow = row;
                     }//end of if (row[0].ToString() == ID)
                 }//end of foreach (DataRow row in ds.Tables[tblName].Rows)
                 for (int i = 0; i < fields.Length - 1; i++)
                 {
                     affectedRow[fields[i]] = values[i];
                 }//end of for (int i = 0; i < fields.Length - 1; i++)
                 ds.Tables[tblName].Rows[ds.Tables[tblName].Rows.IndexOf(affectedRow)].SetModified();
                 builder.GetUpdateCommand();
                 adapter.Update(ds.Tables[tblName]);
                 success = true;
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
             return success;

             //??consider making another dataSet so that current data can be compared to new data and then ask permission to update   ??????

         }//end of public bool updateCmd(string tblName, string ID, string[] fields, ArrayList values)

         public void updateRecCmd(string tblName, string idField, string ID, string[] fields, ArrayList values)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 string statement = "UPDATE ";
                 statement += tblName;
                 statement += " SET ";
                 
                 for (int i = 0; i <= fields.Length - 2; i++)
                 {
                     if (i < fields.Length - 2)
                     {
                         statement += fields[i+1] + " = '" + values[i+1] + "', ";
                     }//end of if (i <= fields.Length - 2)
                     else
                     {
                         statement += fields[i+1] + " = '" + values[i+1]+"'";
                     }//end of else (i < fields.Length - 2)
                 }//end of for (int i = 0; i <= fields.Length - 2; i++)
                 statement += " WHERE ";
                 statement += idField + " = " + ID;
                // DataSet getds;               
                 cmd = new SqlCommand(statement, conn);
               //  adapter = new SqlDataAdapter(cmd);                
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 cmd.ExecuteNonQuery();
                 conn.Close();
                
             }//end of using (SqlConnection conn = new SqlConnection(connstring))
         }//end of public DataTable updateRecCmd(string tblName, string idField, string ID, string[] fields, ArrayList values)

         #endregion
         #region Inventory GetQueries
         public DataTable getInventoryValue()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
               //  DataSet getds;
                 cmd = new SqlCommand("SELECT I.InventoryID,I.InvCode,I.InvItem,I.InvDescription,SI.SSIStockLeft,SI.SSIPrice, sup.SupName FROM Inventory I, SubStockIN SI, InvoiceStockIN ISI, Supplier sup WHERE SI.SSIStockLeft >0 and I.InventoryID=SI.InventoryID and SI.ISIID=ISI.ISIID and isi.SupplierID=sup.SupplierID", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getInventoryValue()

         public DataTable getFIFODatedPrice(string invID)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT ssi.SubStockINID,inv.InventoryID,inv.InvItem,ssi.SSIQuantityIN,ssi.SSIPrice,ssi.ISIID,ssi.SSIStockLeft,isi.ISIInvoiceNo,isi.ISIDateReceived FROM InvoiceStockIN isi, SubStockIN ssi,Inventory inv WHERE ssi.InventoryID='" + invID + "' AND ssi.SSIStockLeft>0 AND inv.InventoryID='" + invID + "'AND isi.ISIID=ssi.ISIID ORDER BY isi.ISIDateReceived ASC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getFIFODatedPrice(string invID)

         public DataTable getAllInvoice()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds; "ISIID", "ISIInvoiceNo", "ISIDateReceived", "SupplierID", "ISIInvoiceTotalIncl" 
                 cmd = new SqlCommand("SELECT isi.ISIID, isi.ISIInvoiceNo, isi.ISIDateReceived, sup.SupName, isi.ISIInvoiceTotalIncl FROM InvoiceStockIN isi, Supplier sup WHERE isi.SupplierID=sup.SupplierID ORDER BY isi.ISIDateReceived DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getCurrentInvoice(string isiID)        

         public DataTable getStockInInvoice(string isiID)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 // "SubStockINID", "InventoryID", "SSIQuantityIN", "SSIPrice", "ISIID", "SSIStockLeft"
                 cmd = new SqlCommand("SELECT inv.InventoryID, inv.InvCode, inv.InvItem,inv.InvDescription,inv.InvSupplierDescription, inv.InvCategory, inv.InvReorderLevel,inv.InvMarkup,ssi.SSIQuantityIN, ssi.SSIPrice, ssi.SubStockINID, isi.ISIID FROM InvoiceStockIN isi, SubStockIN ssi, Inventory inv  WHERE ssi.ISIID='" + isiID + "' AND isi.ISIID='" + isiID + "' AND ssi.InventoryID=inv.InventoryID", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getStockInInvoice(string isiID)

         public DataTable getMaxDatedPrice(string invID)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT TOP 1 ssi.SubStockINID,inv.InventoryID,inv.InvItem,ssi.SSIQuantityIN,ssi.SSIPrice,isi.ISIID,ssi.SSIStockLeft,isi.ISIInvoiceNo,isi.ISIDateReceived FROM InvoiceStockIN isi, SubStockIN ssi,Inventory inv WHERE ssi.InventoryID='" + invID + "' AND ssi.SSIStockLeft>0 AND inv.InventoryID='" + invID + "' AND isi.ISIID=ssi.ISIID ORDER BY ISIDateReceived DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getMaxDatedPrice(string invID)

         public DataTable getLowStock()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT inv.InventoryID,inv.InvCode,inv.InvItem,inv.InvSupplierDescription,inv.InvDescription,inv.InvReorderLevel, tot.ISTotalStock,ssi.SSIPrice FROM Inventory inv, InventoryStock tot,InvoiceStockIN isi, SubStockIN ssi WHERE inv.InventoryID= tot.InventoryID AND inv.InvReorderLevel>=tot.ISTotalStock AND ssi.InventoryID=inv.InventoryID AND isi.ISIID=ssi.ISIID AND isi.ISIDateReceived=(select MAX(ISIDateReceived) from InvoiceStockIN isi)", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getLowStock()

         public DataTable getAllStockWithPrice()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT inv.InventoryID,inv.InvCode,inv.InvItem,inv.InvSupplierDescription,inv.InvDescription,inv.InvReorderLevel, tot.ISTotalStock,inv.InvMarkup FROM Inventory inv, InventoryStock tot WHERE inv.InventoryID= tot.InventoryID", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getAllStockWithPrice()

         public DataTable getAllOrderHistory()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT cast(sup.SupPrefix AS varchar(10)) +'-'+ cast(ord.OrderNumber AS varchar(10)) AS OrderNum, sup.SupName,ord.OrdersDate, ord.OrderEstimateTotal, ord.OrdersID FROM Orders ord,Supplier sup WHERE sup.SupplierID=ord.SupplierID ORDER BY ord.OrdersDate DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getAllOrderHistory()

         public DataTable getSupplierOrderHistory(string supID)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT cast(sup.SupPrefix AS varchar(10)) +'-'+ cast(ord.OrderNumber AS varchar(10)) AS OrderNum, sup.SupName,ord.OrdersDate, ord.OrderEstimateTotal, ord.OrdersID FROM Orders ord,Supplier sup WHERE sup.SupplierID='" + supID + "' AND ord.SupplierID='" + supID + "' ORDER BY ord.OrdersDate DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getSupplierOrderHistory(string supID)

         public DataTable getSupOrderDetails(string orderID)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT subord.InventoryID,inv.InvCode,inv.InvItem,inv.InvSupplierDescription,inv.InvDescription,subord.SOLength,subord.SOOrderedQuantity,subord.SOPrice FROM Inventory inv, SubOrders subord WHERE subord.OrdersID='" + orderID + "' AND subord.InventoryID=inv.InventoryID", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getSupOrderDetails(string supID)

         public DataTable getLastOrderNumber(string supID)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT TOP 1 OrderNumber FROM Orders WHERE SupplierID='" + supID + "' ORDER BY OrderNumber DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getLastOrderNumber(string supID)
         #endregion

         public double getVATrate()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 double vat = 0.00;
                 //  DataSet getds;
                 cmd = new SqlCommand("SELECT SetValue FROM Settings WHERE SetCode='VAT'", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 vat = double.Parse(result.Rows[0][0].ToString());
                 return vat;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public double getVATrate()

         public DataTable getSPLogin(string username, string password)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                DataTable result = new DataTable();
               //  DataSet getds;
               //  cmd = new SqlCommand("Select * from " + tblName, conn);
               //  adapter = new SqlDataAdapter(cmd);
               //  SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
               //  conn.Open();
               //  getds = new DataSet();
               //  adapter.Fill(getds, tblName);
               //  conn.Close();
               //  result = getds.Tables[0];
                 return result;


               ////  connection.Open();
               //  cmd = new SqlCommand("spLoginCheck", conn);
               //  cmd.CommandType = CommandType.StoredProcedure;
               //  cmd.Parameters.AddWithValue("@username", username);
               //  cmd.Parameters.AddWithValue("@@password", password);
               //  cmd.ExecuteNonQuery();
               //  conn.Close();

             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getSPLogin(string tblName)

         public DataTable getSPPermissions(string tblName)
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 DataSet getds;
                 cmd = new SqlCommand("Select * from " + tblName, conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 getds = new DataSet();
                 adapter.Fill(getds, tblName);
                 conn.Close();
                 result = getds.Tables[0];
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getSPPermissions(string tblName)

         public DataTable getLastStockInvoice()
         {
             using (SqlConnection conn = new SqlConnection(connString))
             {
                 DataTable result = new DataTable();
                 string stockString = "STOCK";
                 cmd = new SqlCommand("SELECT TOP 1 ISIInvoiceNo FROM InvoiceStockIN WHERE ISIInvoiceNo LIKE '" + stockString + " %' ORDER BY ISIInvoiceNo DESC", conn);
                 adapter = new SqlDataAdapter(cmd);
                 SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
                 conn.Open();
                 adapter.Fill(result);
                 conn.Close();
                 return result;
             }//end of using (SqlConnection conn = new SqlConnection(connString))
         }//end of public DataTable getLastStockInvoice()
    }//end of public class DataAccess
 }//end of namespace ImagineTrailvan
