using ClosedXML.Excel;
using Common;
using CommonModels;
using DataManager;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FoodRepository
    {
        public OperationResponse Getfoodcatagories(int catid)
        {
            OperationResponse OR = new OperationResponse();
            List<SqlParameter> lstparam = new List<SqlParameter>();
            DBHelper dh = new DBHelper();
            int status = 0;
            string massage = "";

            lstparam.Add(new SqlParameter("@In_Catid", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = catid });
            try
            {
                DataTable dt = dh.ExecuteDataTable("GetallFoodCatagories", lstparam, null, CommandType.StoredProcedure);
                List<FoodCategory> li = new List<FoodCategory>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        li.Add(new FoodCategory
                        {
                            CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                            CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                            ImagePath= Convert.ToString(dt.Rows[i]["ImagePath"]),
                            IsActive= Convert.ToInt32(dt.Rows[i]["IsActive"])
                        });
                    }
                    OR.Data = li;
                    OR.Status = status;
                    OR.Message = massage;
                }
            }
            catch (Exception ex)
            {
                OR.Data = null;
                OR.Status = status;
                OR.Message = massage;
            }

            return OR;
        }

        public OperationResponse Getfooditem(int Categoyid)
        {
            OperationResponse OR = new OperationResponse();
            List<SqlParameter> lstparam = new List<SqlParameter>();
            DBHelper dh = new DBHelper();
            int status = 0;
            string massage = "";

            lstparam.Add(new SqlParameter("@In_Catid", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Categoyid });
            try
            {
                DataTable dt = dh.ExecuteDataTable("GetallFooditems", lstparam, null, CommandType.StoredProcedure);
                List<Food> li = new List<Food>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        li.Add(new Food
                        {
                            FoodID = Convert.ToInt32(dt.Rows[i]["FoodId"]),
                            CategoryID = Convert.ToInt32(dt.Rows[i]["CategoryId"]),
                            CategoryName = Convert.ToString(dt.Rows[i]["CategoryName"]),
                            FoodName = Convert.ToString(dt.Rows[i]["FoodName"]),
                            Description = Convert.ToString(dt.Rows[i]["Description"]),
                            Price = Convert.ToDecimal(dt.Rows[i]["Price"]),
                            ImageURL = Convert.ToString(dt.Rows[i]["ImagePath"]),
                        });
                    }
                    OR.Data = li;
                    OR.Status = status;
                    OR.Message = massage;
                }
            }
            catch (Exception ex)
            {
                OR.Data = null;
                OR.Status = status;
                OR.Message = massage;
            }

            return OR;
        }

        public OperationResponse AddEditFoodCategory(FoodCategory Fcdata)
        {
            OperationResponse OR = new OperationResponse();
            List<SqlParameter> lstparam = new List<SqlParameter>();
            int Status = 0;
            string Message = "";
            DBHelper dh = new DBHelper();
            lstparam.Add(new SqlParameter("@In_CategoryId", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Fcdata.CategoryId });
            lstparam.Add(new SqlParameter("@In_CategoryName", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = string.IsNullOrWhiteSpace(Fcdata.CategoryName) ? (object)DBNull.Value : Fcdata.CategoryName });
            lstparam.Add(new SqlParameter("@In_Description", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = "" });
            lstparam.Add(new SqlParameter("@In_ImagePath", SqlDbType.VarChar) { Direction = ParameterDirection.Input, Value = string.IsNullOrWhiteSpace(Fcdata.ImagePath) ? (object)DBNull.Value : Fcdata.ImagePath });
            lstparam.Add(new SqlParameter("@In_IsActive", SqlDbType.Int) { Direction = ParameterDirection.Input, Value = Fcdata.IsActive });


            lstparam.Add(new SqlParameter("@Retvalue", SqlDbType.Int) { Direction = ParameterDirection.Output });
            lstparam.Add(new SqlParameter("@GeneratedClientId", SqlDbType.VarChar, 200) { Direction = ParameterDirection.Output });
            try
            {
                DataTable dt = dh.ExecuteDataTable("AddEditFoodCategory", lstparam, null, CommandType.StoredProcedure, out Status, out Message);

                OR.Data = null;
                OR.Status = Status;
                OR.Message = Message;
            }
            catch (Exception ex)
            {
                OR.Data = null;
                OR.Status = 0;
                OR.Message = ex.Message;
            }

            return OR;
        }

        }
}
