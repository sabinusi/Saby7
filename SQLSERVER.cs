using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

// Imports Microsoft.SqlServer.Management.Common
/// <summary>

/// ''' The Data namespace.

/// ''' </summary>
namespace Saby7
{
    /// <summary>
    ///     ''' Calss for managing SQLs for Database(SQL Server).
    ///     ''' Created by Annael Samwel
    ///     ''' </summary>
    public sealed class SQLSERVER : object
    {
        /// <summary>
        ///         ''' Initializes a new instance of the <see cref="SQLSERVER"/> class.
        ///         ''' </summary>
        ///         ''' <exception cref="Exception">Devpp DLL not registered</exception>
        public SQLSERVER(String connectionstring)
        {
            ConnectionString = connectionstring;
        }
        /// <summary>
        ///         ''' The table devpp user
        ///         ''' </summary>
        private DataTable tblDevppUser;
        /// <summary>
        ///         ''' The _FRM utility
        ///         ''' </summary>

        /// <summary>
        ///         ''' The exa instance
        ///         ''' </summary>
        private static string exaInstance = null;
        /// <summary>
        ///         ''' The exa database
        ///         ''' </summary>
        private static string exaDatabase = null;
        /// <summary>
        ///         ''' The exa instance ii
        ///         ''' </summary>
        private static string exaInstanceII = exaInstance;
        /// <summary>
        ///         ''' The exa database ii
        ///         ''' </summary>
        private static string exaDatabaseII = exaDatabase;
        /// <summary>
        ///         ''' The exa username
        ///         ''' </summary>
        private static string exaUsername = "sa";
        /// <summary>
        ///         ''' The exa password
        ///         ''' </summary>
        private static string exaPassword = "!ob1Devpp2008";
        /// <summary>
        ///         ''' The sa password
        ///         ''' </summary>
        private static string saPassword = "evisa@2017";
        /// <summary>
        ///         ''' The exa windows authentication
        ///         ''' </summary>
        private static bool exaWindowsAuth = false;
        /// <summary>
        ///         ''' The exa windows authentication ii
        ///         ''' </summary>
        private static bool exaWindowsAuthII = false;
        /// <summary>
        ///         ''' The exa connection string
        ///         ''' </summary>
        private static string exaConnString = "";
        /// <summary>
        ///         ''' The exa connection string ii
        ///         ''' </summary>
        private static string exaConnStringII;
        /// <summary>
        ///         ''' The _ get current date
        ///         ''' </summary>
        private static DateTime _GetCurrentDate;
        /// <summary>
        ///         ''' The _ image connection string
        ///         ''' </summary>
        private static string _ImageConnectionString;
        /// <summary>
        ///         ''' The version
        ///         ''' </summary>
        private const string Version = "CREATE TABLE [dbo].[tblDevppVersion]([Devpp] [int] NULL, [Client] [int] NULl ) ON [PRIMARY]";
        /// <summary>
        ///         ''' The parameter command
        ///         ''' </summary>
        private const string ParamCmd = "SELECT parm.name AS Parameter, " + "typ.name AS [Type]  " + "FROM sys.procedures sp " + "JOIN sys.parameters parm ON sp.object_id = parm.object_id " + "JOIN sys.types typ ON parm.system_type_id = typ.system_type_id  " + " WHERE typ.name <> 'sysname' and sp.name = @spName order by parm.parameter_id";
        /// <summary>
        ///         ''' The SQLTBL devpp user
        ///         ''' </summary>
        private const string sqltblDevppUser = "SELECT clm.name FROM sys.tables tbl  JOIN sys.columns clm ON tbl.object_id = clm.object_id " + "WHERE tbl.Name = 'tblDevppUser'";


        /// <summary>
        ///         ''' The curent date
        ///         ''' </summary>
        private const string CurentDate = "SELECT getDate() AS [CURRENTDATE]";

        /// <summary>
        ///         ''' The createtbl devpp user
        ///         ''' </summary>
        private const string createtblDevppUser = "CREATE TABLE [dbo].[tblDevppUser]( " + "[usrPid] [int] IDENTITY(1,1) NOT NULL,  " + "[usrName] [nvarchar](50) NOT NULL,  " + "[usrPassword] [int] NULL, " + "[PepId] [int] NULL CONSTRAINT [DF_tblDevppUser_PepId]  DEFAULT ((0)), " + "[usrIsGroup] [bit] NULL CONSTRAINT [DF_tblDevppUser_IsGroup]  DEFAULT ((0)), " + "[usrIsBlocked] [bit] NULL CONSTRAINT [DF_tblDevppUser_IsBlocked]  DEFAULT ((0)), " + "[usrDomain] [int] NULL CONSTRAINT [DF_tblDevppUser_usrDomain]  DEFAULT ((0)), " + "[usrLanguage] [int] NULL CONSTRAINT [DF_tblDevppUser_usrLanguage]  DEFAULT ((0)), " + "[usrRetryBlock] [bit] NULL CONSTRAINT [DF_tblDevppUser_usrRetries]  DEFAULT ((0)), " + "[usrPasswordValidity] [int] NULL CONSTRAINT [DF_tblDevppUser_usrPasswordValidity]  DEFAULT ((0)), " + "[usrPasswordChanged] [smalldatetime] NULL, " + "[usrStrongPassword] [bit] NULL CONSTRAINT [DF_tblDevppUser_usrStrongPassword]  DEFAULT ((0)), " + "[usrBackColour1] [bigint] NULL CONSTRAINT [DF_tblDevppUser_usrBackColour1]  DEFAULT ((0)), " + "[usrBackColour2] [bigint] NULL CONSTRAINT [DF_tblDevppUser_usrBackColour2]  DEFAULT ((0)), " + "[usrFontColour] [bigint] NULL CONSTRAINT [DF_tblDevppUser_usrFontColour]  DEFAULT ((0)), " + "[usrFlag] [int] NULL CONSTRAINT [DF_tblDevppUser_usrFlag]  DEFAULT ((0)), " + "[usrCreateUserID] [int] NULL CONSTRAINT [DF_tblDevppUser_usrCreateUserID]  DEFAULT ((0)), " + "[usrAuditCreateDate] [datetime] NULL CONSTRAINT [DF_tblDevppUser_AuditCreateDate]  DEFAULT (getdate()), " + "[usrFontName] [nvarchar](50) NULL CONSTRAINT [DF_tblDevppUser_usrFontName]  DEFAULT (N'N''Microsoft Sans Serif'), " + "[usrFontSize] [decimal](18, 1) NULL CONSTRAINT [DF_tblDevppUser_usrFontSize]  DEFAULT ((8)), " + "[usrIBold] [bit] NULL CONSTRAINT [DF_tblDevppUser_usrIBold]  DEFAULT ((0)), " + "CONSTRAINT [PK_tblDevppUser] PRIMARY KEY CLUSTERED  " + "( [usrPid] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY]";

        /// <summary>
        ///         ''' The table devpp user rights
        ///         ''' </summary>
        private const string tblDevppUserRights = "CREATE TABLE [dbo].[tblDevppUserRights](" + "[UserID] [int] NOT NULL, [UserSecNr] [int] NOT NULL, 	[UserSecValue] [int] NULL, [UserCreateID] [int] NULL, " + "[AuditCreateDate] [datetime] NULL, [UserUpdateID] [int] NULL, [AuditUpdateDate] [datetime] NULL, CONSTRAINT [PK_tblDevppUserSecurity] PRIMARY KEY CLUSTERED " + "(	[UserID] ASC,	[UserSecNr] ASC " + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY] ALTER TABLE [dbo].[tblDevppUserRights]  WITH CHECK ADD  CONSTRAINT [FK_tblDevppUserRights_tblDevppUser] FOREIGN KEY([UserID]) " + "REFERENCES [dbo].[tblDevppUser] ([usrPid]) ALTER TABLE [dbo].[tblDevppUserRights] CHECK CONSTRAINT [FK_tblDevppUserRights_tblDevppUser] ";

        /// <summary>
        ///         ''' The table devpp user maping
        ///         ''' </summary>
        private const string tblDevppUserMaping = "CREATE TABLE [dbo].[tblDevppMapUserGroup]( [UserId] [int] NOT NULL, [GroupId] [int] NOT NULL, " + "CONSTRAINT [PK_tblDevppMapUserGroup] PRIMARY KEY CLUSTERED ( [UserId] ASC, [GroupId] ASC " + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY] ALTER TABLE [dbo].[tblDevppMapUserGroup]  WITH CHECK ADD  CONSTRAINT [FK_tblDevppMapUserGroup_tblDevppUser] FOREIGN KEY([UserId]) " + " REFERENCES [dbo].[tblDevppUser] ([usrPid]) ALTER TABLE [dbo].[tblDevppMapUserGroup] CHECK CONSTRAINT [FK_tblDevppMapUserGroup_tblDevppUser] " + "ALTER TABLE [dbo].[tblDevppMapUserGroup]  WITH CHECK ADD  CONSTRAINT [FK_tblDevppMapUserGroup_tblDevppUser1] FOREIGN KEY([GroupId]) " + "REFERENCES [dbo].[tblDevppUser] ([usrPid]) ALTER TABLE [dbo].[tblDevppMapUserGroup] CHECK CONSTRAINT [FK_tblDevppMapUserGroup_tblDevppUser1] ";

        /// <summary>
        ///         ''' The table devpp defaults
        ///         ''' </summary>
        private const string tblDevppDefaults = "CREATE TABLE [dbo].[tblDevppDefaults]( [DefaultID] [int] NOT NULL, [DefInt] [int] NULL CONSTRAINT [DF_tblDevppDefaults_DefInt]  DEFAULT ((0)), " + "[DefBit] [bit] NULL, [DefFloat] [float] NULL, [DefDate] [datetime] NULL, [DefNvarchar] [nvarchar](max) NULL, [DefImageID] [int] NULL, " + "CONSTRAINT [PK_tblDevppDefaults] PRIMARY KEY CLUSTERED  ( [DefaultID] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + " ) ON [PRIMARY] ";


        /// <summary>
        ///         ''' The table image
        ///         ''' </summary>
        private const string tblImage = "CREATE TABLE [dbo].[tblImage]( [ImageID] [int] IDENTITY(1,1) NOT NULL, [ImageValue] [varbinary](max) NOT NULL,  " + "CONSTRAINT [PK_tblImage] PRIMARY KEY CLUSTERED (  [ImageID] ASC ) " + "WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY]";

        /// <summary>
        ///         ''' The table devpp attribute type
        ///         ''' </summary>
        private const string tblDevppAttrType = "CREATE TABLE [dbo].[tblDevppAttrType]( [AttrTypeId] [int]  NOT NULL, [AttrTypeDescription] [nvarchar](50) NULL, " + "[AttrTypeHasCode] [bit] NULL, [isBlocked] [bit] NULL CONSTRAINT [DF_tblDevppAttrType_isBlocked]  DEFAULT ((0)), [isSystem] [bit] NULL CONSTRAINT [DF_tblDevppAttrType_isSystem]  DEFAULT ((0)), CONSTRAINT [PK_tblDevppAttrType] PRIMARY KEY CLUSTERED  " + "( [AttrTypeId] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY]";

        /// <summary>
        ///         ''' The table devpp attribute
        ///         ''' </summary>
        private const string tblDevppAttr = "CREATE TABLE [dbo].[tblDevppAttr]( [attrId] [int] IDENTITY(1,1) NOT NULL, [AttrTypeId] [int] NULL, [AttrDescription] [nvarchar](50) NULL, " + "[AttrIsBlocked] [bit] NULL CONSTRAINT [DF_tblDevppAttr_AttrIsBlocked]  DEFAULT ((0)), [AttrIsSystem] [bit] NULL CONSTRAINT [DF_tblDevppAttr_AttrIsSystem]  DEFAULT ((0)), " + "[AttrCode] [nvarchar](8) NULL, CONSTRAINT [PK_tblDevppAttr] PRIMARY KEY CLUSTERED  ( [attrId] ASC )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY] ";



        /// <summary>
        ///         ''' The sp devpp get login
        ///         ''' </summary>
        private const string spDevppGetLogin = "CREATE PROC uspDevppGetLogin(@mode int, @UserName nvarchar(50), @usrPassword int) AS " + "if @mode = 3 SELECT [usrDomain] ,[usrIsBlocked],[usrPid], [usrPasswordChanged], [usrPasswordValidity], [usrRetryBlock] FROM [tblDevppUser] WHERE  [usrName] =@UserName if @mode = 0 " + " SELECT * FROM [tblDevppUser] WHERE  [usrName] =@UserName  " + " if (@mode = 1) or (@mode = 2) SELECT * FROM [tblDevppUser]  " + " WHERE  ([usrName] =@UserName) AND ([usrPassword] = @usrPassword) ";

        /// <summary>
        ///         ''' The table devpp user log
        ///         ''' </summary>
        private const string tblDevppUserLog = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDevppUserLog]') AND type in (N'U')) " + "BEGIN " + "CREATE TABLE [dbo].[tblDevppUserLog]( " + "[RecID] [int] IDENTITY(1,1) NOT NULL, " + "[MachineName] [nvarchar](50) NULL, " + "[UserName] [nvarchar](50) NULL, " + "[NetAddress] [nvarchar](50) NULL, " + "[Date] [datetime] NULL, " + "[Description] [nvarchar](50) NULL, " + "CONSTRAINT [PK_tblDevppUserLog] PRIMARY KEY CLUSTERED  " + "( [RecID] ASC " + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY]  END ";

        /// <summary>
        ///         ''' The table contacts
        ///         ''' </summary>
        private const string tblContacts = "CREATE TABLE [dbo].[tblContacts]([IdNumber] [int] IDENTITY(1,1) NOT NULL, " + "[Company] [int] NULL, [FirstName] [nvarchar](50) NULL, [LastName] [nvarchar](50) NULL, " + "[Position] [nvarchar](50) NULL, [Telephone] [nvarchar](50) NULL, [Fax] [nvarchar](50) NULL, " + "[Mobile] [nvarchar](50) NULL, [email] [nvarchar](50) NULL, [Address] [nvarchar](50) NULL, [POBox] [nvarchar](50) NULL, [City] [nvarchar](50) NULL, [Country] [nvarchar](50) NULL, " + "[Notes] [nvarchar](1000) NULL, [IsBlocked] [bit] NULL, [Flag] [bit] NULL, [IsUsed] [bit] NULL, " + "[bdeleted] [bit] NULL, [PepFirstName] [int] NULL, [PepLastName] [int] NULL, " + "[PepEmail] [int] NULL, [PepAddress] [int] NULL, " + "[PepCity] [int] NULL, [PepCountry] [int] NULL, " + " CONSTRAINT [PK_tblContacts] PRIMARY KEY CLUSTERED  " + "( [IdNumber] ASC " + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] " + ") ON [PRIMARY]";

        /// <summary>
        ///         ''' The sq LCMD
        ///         ''' </summary>
        private static string SQLcmd;
        /// <summary>
        ///         ''' The _ SQL connection
        ///         ''' </summary>
        private static SqlConnection _SQLConn = new SqlConnection();
        /// <summary>
        ///         ''' The _ SQL connection ii
        ///         ''' </summary>
        private static SqlConnection _SQLConnII = new SqlConnection();
        /// <summary>
        ///         ''' The command
        ///         ''' </summary>
        private SqlCommand cmd = new SqlCommand(SQLcmd);
        /// <summary>
        ///         ''' The _usp name
        ///         ''' </summary>
        private static string _uspName;
        /// <summary>
        ///         ''' The _ return rows
        ///         ''' </summary>
        private static int _ReturnRows;


        /// <summary>
        ///         ''' Gets or sets the FRM utility.
        ///         ''' </summary>
        ///         ''' <value>The FRM utility.</value>


        /// <summary>
        ///         ''' This is the get property returns database date and time
        ///         ''' </summary>
        ///         ''' <value>The get current date.</value>
        public static DateTime GetCurrentDate
        {
            get
            {
                return _GetCurrentDate;
            }
        }
        /// <summary>
        ///         ''' This is the get property returns number of rows for execution of stored procedure.
        ///         ''' </summary>
        ///         ''' <value>The return rows.</value>

        public static int ReturnRows
        {
            get
            {
                return _ReturnRows;
            }
        }


        /// <summary>
        ///         ''' Gets or sets the name of the server.
        ///         ''' </summary>
        ///         ''' <value>The name of the server.</value>
        internal static string ServerName
        {
            get
            {
                return exaInstance;
            }
            set
            {
                exaInstance = value;
            }
        }
        /// <summary>
        ///         ''' Gets or sets the name of the database.
        ///         ''' </summary>
        ///         ''' <value>The name of the database.</value>
        internal static string DatabaseName
        {
            get
            {
                return exaDatabase;
            }
            set
            {
                exaDatabase = value;
            }
        }
        /// <summary>
        ///         ''' Gets or sets the server name ii.
        ///         ''' </summary>
        ///         ''' <value>The server name ii.</value>
        internal static string ServerNameII
        {
            get
            {
                return exaInstanceII;
            }
            set
            {
                exaInstanceII = value;
            }
        }
        /// <summary>
        ///         ''' Gets or sets the database name ii.
        ///         ''' </summary>
        ///         ''' <value>The database name ii.</value>
        internal static string DatabaseNameII
        {
            get
            {
                return exaDatabaseII;
            }
            set
            {
                exaDatabaseII = value;
            }
        }
        /// <summary>
        ///         ''' Gets or sets a value indicating whether [selected win authentication].
        ///         ''' </summary>
        ///         ''' <value><c>true</c> if [selected win authentication]; otherwise, <c>false</c>.</value>
        public static bool SelectedWinAuth
        {
            get
            {
                return exaWindowsAuth;
            }
            set
            {
                exaWindowsAuth = value;
            }
        }
        /// <summary>
        ///         ''' Gets or sets a value indicating whether [selected authentification ii].
        ///         ''' </summary>
        ///         ''' <value><c>true</c> if [selected authentification ii]; otherwise, <c>false</c>.</value>
        internal static bool SelectedAuthentificationII
        {
            get
            {
                return exaWindowsAuthII;
            }
            set
            {
                exaWindowsAuthII = value;
            }
        }
        /// <summary>
        ///         ''' Set and Get property returns database username.
        ///         ''' </summary>
        ///         ''' <value>The selected username.</value>
        public static string SelectedUsername
        {
            get
            {
                return exaUsername;
            }
            set
            {
                exaUsername = value;
            }
        }
        /// <summary>
        ///         ''' This is the Set and Get property for database password
        ///         ''' </summary>
        ///         ''' <value>The selected password.</value>
        public static string SelectedPassword
        {
            get
            {
                return exaPassword;
            }
            set
            {
                exaPassword = value;
            }
        }
        /// <summary>
        ///         ''' This is the Set and Get Property for connection string.
        ///         ''' </summary>
        ///         ''' <value>The connection string.</value>
        ///         ''' <exception cref="Exception">Devpp DLL not registered</exception>
        public string ConnectionString;
       
        /// <summary>
        ///         ''' Gets or sets the connection string ii.
        ///         ''' </summary>
        ///         ''' <value>The connection string ii.</value>
        public static string ConnectionStringII
        {
            get
            {
                return exaConnStringII;
            }
            set
            {
                exaConnStringII = value;
            }
        }
        // Friend Shared Property WinAuth() As Boolean
        // Get
        // Return exaWindowsAuth
        // End Get
        // Set(ByVal value As Boolean)
        // exaWindowsAuth = value

        // End Set
        // End Property
        /// <summary>
        ///         ''' Gets or sets a value indicating whether [win authentication ii].
        ///         ''' </summary>
        ///         ''' <value><c>true</c> if [win authentication ii]; otherwise, <c>false</c>.</value>
        internal static bool WinAuthII
        {
            get
            {
                return exaWindowsAuthII;
            }
            set
            {
                exaWindowsAuthII = value;
            }
        }
        /// <summary>
        ///         ''' This is the Set and Get Property for SQL Connection
        ///         ''' </summary>
        ///         ''' <value>The SQL connection.</value>
        public static SqlConnection SQLConn
        {
            get
            {
                return _SQLConn;
            }
            set
            {
                _SQLConn = value;
            }
        }



        /// <summary>
        ///         ''' Function for chacking connectivity.
        ///         ''' </summary>
        ///         ''' <returns>True or False</returns>
        public static bool CheckServerConnection()
        {
            return checkSvrConn();
        }
      
        /// <summary>
        ///         ''' Checks the SVR connection.
        ///         ''' </summary>
        ///         ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool checkSvrConn()
        {
            try
            {
                if (SelectedWinAuth == true)
                    // Dim srvConn = New ServerConnection(exaInstance)
                    // Dim srv = New Server(srvConn)
                    // srvConn.Connect()
                    return true;
                else
                    // Dim srvConn = New ServerConnection(exaInstance, exaUsername, exaPassword)
                    // srvConn.LoginSecure = False
                    // Dim srv = New Server(srvConn)

                    // srvConn.Connect()
                    return true;
            }
            // Dim srvConn = New ServerConnection(exaInstance, exaUsername, exaPassword)
            // Dim srv = New Server(srvConn)
            // srvConn.Connect()
            // Return True
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        ///         ''' Checks the SVR connection ii.
        ///         ''' </summary>
        ///         ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///         ''' <exception cref="Exception"></exception>
        private static bool checkSvrConnII()
        {
            try
            {
                if (WinAuthII == true)
                    return true;
                else
                    return true;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///         ''' Gets the type of the database.
        ///         ''' </summary>
        ///         ''' <param name="value">The value.</param>
        ///         ''' <returns>System.Int32.</returns>
        internal static int GetDbType(string value)
        {
            int @int = 21;

            switch (value.ToString())
            {
                case "bigint":
                    {
                        return 0;
                    }

                case "binary":
                    {
                        return 1;
                    }

                case "bit":
                    {
                        return 2;
                    }

                case "char":
                    {
                        return 3;
                    }

                case "datetime":
                    {
                        return 4;
                    }

                case "decimal":
                    {
                        return 5;
                    }

                case "float":
                    {
                        return 6;
                    }

                case "image":
                    {
                        return 7;
                    }

                case "int":
                    {
                        return 8;
                    }

                case "money":
                    {
                        return 9;
                    }

                case "nchar":
                    {
                        return 10;
                    }

                case "ntext":
                    {
                        return 11;
                    }

                case "nvarchar":
                    {
                        return 12;
                    }

                case "real":
                    {
                        return 13;
                    }

                case "smalldatetime":
                    {
                        return 15;
                    }

                case "smallint":
                    {
                        return 16;
                    }

                case "smallmoney":
                    {
                        return 17;
                    }

                case "text":
                    {
                        return 18;
                    }

                case "timestamp":
                    {
                        return 19;
                    }

                case "varchar":
                    {
                        return 21;
                    }

                case "varbinary":
                    {
                        return 22;
                    }

                case "date":
                    {
                        return 31;
                    }

                case "time":
                    {
                        return 32;
                    }

                case "uniqueidentifier":
                    {
                        return 14;
                    }
            }
            return @int;
        }
        /// <summary>
        ///         ''' Reads the storedprocedure ii.
        ///         ''' </summary>
        ///         ''' <param name="spName">Name of the sp.</param>
        ///         ''' <param name="spValue">The sp value.</param>
        ///         ''' <exception cref="Exception">
        ///         ''' </exception>
        private void ReadStoredprocedureII(string spName, params object[] spValue)
        {
            try
            {
                checkSvrConnII();
                SqlConnection con = new SqlConnection(ConnectionStringII);
                SqlCommand uspParamCmd = new SqlCommand(ParamCmd, con);
                uspParamCmd.Parameters.Add("@spName", SqlDbType.NVarChar);
                uspParamCmd.Parameters["@spName"].Value = spName;
                DataTable uspTable = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(uspParamCmd);
                adp.Fill(uspTable);
                int i = 0;
                if (uspTable.Rows.Count > 0)
                {
                    if (spValue.Length != uspTable.Rows.Count)
                        throw new Exception(spName + " has " + System.Convert.ToString(uspTable.Rows.Count) + ", Only " + System.Convert.ToString(spValue.Length) + "have been passed");
                }
                int x = 0;
                cmd = new SqlCommand();
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                foreach (DataRow rw in uspTable.Rows)
                {
                    if (rw[1].ToString() != "sysname")
                    {
                        cmd.Parameters.Add(rw[0].ToString(), (SqlDbType)GetDbType(rw[1].ToString()));

                        cmd.Parameters[rw[0].ToString()].Value = SecurityElement.Escape(spValue[x].ToString());
                    }


                    x += 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///         ''' Reads the storedprocedure.
        ///         ''' </summary>
        ///         ''' <param name="spName">Name of the sp.</param>
        ///         ''' <param name="spValue">The sp value.</param>
        ///         ''' <exception cref="Exception">
        ///         ''' Devpp DLL not registered
        ///         ''' or
        ///         ''' or
        ///         ''' </exception>
        private void ReadStoredprocedure(string spName, params object[] spValue)
        {
            try
            {
                GC.Collect();
                SqlConnection con = new SqlConnection(ConnectionString);
                SqlCommand uspParamCmd = new SqlCommand(ParamCmd, con);
                uspParamCmd.Parameters.Add("@spName", SqlDbType.NVarChar);
                uspParamCmd.Parameters["@spName"].Value = spName;
                DataTable uspTable = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(uspParamCmd);
                adp.Fill(uspTable);
                int i = 0;
                if (uspTable.Rows.Count > 0)
                {
                    if (spValue.Length != uspTable.Rows.Count)
                    {
                        uspTable = null;
                        throw new Exception(spName + " has " + System.Convert.ToString(uspTable.Rows.Count) + ", Only " + System.Convert.ToString(spValue.Length) + "have been passed");
                    }
                }
                int x = 0;
                cmd = new SqlCommand();
                cmd.CommandTimeout = 30;
                cmd.CommandText = spName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;
                foreach (DataRow rw in uspTable.Rows)
                {
                    if (rw[1].ToString () != "sysname")
                    {
                        cmd.Parameters.Add(rw[0].ToString(), (SqlDbType)GetDbType(rw[1].ToString ()));
                        if (spValue[x].GetType() == typeof(string))
                            cmd.Parameters[rw[0].ToString()].Value = SecurityElement.Escape(spValue[x].ToString());
                        else
                            cmd.Parameters[rw[0].ToString()].Value = spValue[x];
                    }


                    x += 1;
                }
                uspTable = null;
            }
            catch (Exception ex)
            {
               
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///         ''' Execunting stored procedure and return Datatable
        ///         
        ///         ''' </summary>
        ///         ''' <param name="spName">Stored procedure name.</param>
        ///         ''' <param name="spValue">Parameters for stored propcedure.</param>
        ///         ''' <returns>Datatable</returns>
        ///         ''' <exception cref="Exception"></exception>
        public DataTable GetSPDataTable(string spName, params object[] spValue)
        {
            DataTable dtTable = new DataTable(spName);
            try
            {
                ReadStoredprocedure(spName, spValue);

                if (cmd == null)
                    return dtTable;
                SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
                sqlAdp.Fill(dtTable);
                sqlAdp.Dispose();
                sqlAdp = null;
                cmd.Dispose();
                cmd = null;
            }
            catch (Exception ex)
            {

                dtTable = null;
                cmd.Dispose();
                
                    throw new Exception(ex.Message);
            }
            finally
            {
            }
            return dtTable;
        }
        /// <summary>
        ///         ''' Gets the sp data table.
        ///         ''' </summary>
        ///         ''' <param name="spName">Name of the sp.</param>
        ///         ''' <param name="colNameList">The col name list.</param>
        ///         ''' <param name="ColType">Type of the col.</param>
        ///         ''' <param name="addAtEnd">if set to <c>true</c> [add at end].</param>
        ///         ''' <param name="spValue">The sp value.</param>
        ///         ''' <returns>DataTable.</returns>
        ///         ''' <exception cref="Exception">
        ///         ''' Number of items in colName must be equal to coltype
        ///         ''' or
        ///         ''' Number of items in colName must be equal to coltype
        ///         ''' or
        ///         ''' </exception>
        public DataTable GetSPDataTable2(string spName, string[] colNameList, bool addAtEnd, params object[] spValue)
        {
            ReadStoredprocedure(spName, spValue);
            DataTable dtTable = new DataTable(spName);
            SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
            sqlAdp.Fill(dtTable);
            try
            {
                if (addAtEnd == false)
                {
                }


                if (addAtEnd == true)
                {
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dtTable;
        }
        /// <summary>
        ///         ''' Execute stored procedure and returns SQL Command
        ///         ''' </summary>
        ///         ''' <param name="spName">Stored procedure name</param>
        ///         ''' <param name="spValue">Stored procedure parameter values</param>
        ///         ''' <returns>SQL Command</returns>
        ///         ''' <exception cref="Exception"></exception>
        public SqlCommand GetSPSQLCom(string spName, params object[] spValue)
        {
            ReadStoredprocedure(spName, spValue);

            try
            {
                return cmd;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        ///         ''' Execute stored procedure and returns true or false
        ///         ''' </summary>
        ///         ''' <param name="spName">Stored procedure name</param>
        ///         ''' <param name="spValue">Stored procedure parameter values</param>
        ///         ''' <returns>True or False</returns>
        ///         ''' <exception cref="Exception"></exception>
        public bool GetSPBoolean(string spName, params object[] spValue)
        {
            ReadStoredprocedure(spName, spValue);
            DataTable dtTable = new DataTable();
            try
            {
                SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
                sqlAdp.Fill(dtTable);
                dtTable.Dispose();
                dtTable = null;
                sqlAdp.Dispose();
                sqlAdp = null;
                cmd.Dispose();
                cmd = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }
        /// <summary>
        ///         ''' Runusps the ret bol ii.
        ///         ''' </summary>
        ///         ''' <param name="spName">Name of the sp.</param>
        ///         ''' <param name="spValue">The sp value.</param>
        ///         ''' <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        ///         ''' <exception cref="Exception"></exception>
        public bool RunuspRetBolII(string spName, params object[] spValue)
        {
            ReadStoredprocedureII(spName, spValue);
            DataTable dtTable = new DataTable();
            try
            {
                SqlDataAdapter sqlAdp = new SqlDataAdapter(cmd);
                sqlAdp.Fill(dtTable);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        
        
      
       
    }
}
