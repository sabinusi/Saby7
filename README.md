

 using Saby7
 
 SQLSERVER desktopDataInstance = 
 new SQLSERVER(ConfigurationManager.ConnectionStrings["connectionStringNameGoeshere"].ConnectionString);

DataTable dataTable = desktopDataInstance.GetSPDataTable("storedprocedurenamegoes here");

    List<ExpectedObject> myList = new List<ExpectedObject>();
	
       if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow item in dataTable.Rows)
                    {

                        myList.Add(DatatableConvertor.ToObject<ExpectedObject>(item));
                    }

                }


