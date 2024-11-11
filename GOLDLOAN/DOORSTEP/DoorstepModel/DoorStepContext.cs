using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DOORSTEP.DoorstepModel;

public partial class DoorStepContext : DbContext
{
    public DoorStepContext()
    {
    }

    public DoorStepContext(DbContextOptions<DoorStepContext> options)
        : base(options)
    {
    }
    public virtual DbSet<HrmTourDtl> HrmTourDtls { get; set; }
    public virtual DbSet<DoorstepSplash> DoorstepSplashes { get; set; }
    public virtual DbSet<PledgeScheme> PledgeScheme { get; set; }
    public virtual DbSet<Dual> Duals { get; set; } = null!;
    public virtual DbSet<BranchMaster> BranchMasters { get; set; }
    public virtual DbSet<BlockedDevice> BlockedDevices { get; set; }
    public virtual DbSet<DistrictMaster> DistrictMasters { get; set; }
    public virtual DbSet<TblDoorstepConfiguration> TblDoorstepConfigurations { get; set; }

    public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; }

    public virtual DbSet<KeyMaster> KeyMasters { get; set; }

    public virtual DbSet<PostMaster> PostMasters { get; set; }

    public virtual DbSet<StateMaster> StateMasters { get; set; }

    public virtual DbSet<TblDoorstepCustMst> TblDoorstepCustMsts { get; set; }

    public virtual DbSet<TblDoorstepOtpLog> TblDoorstepOtpLogs { get; set; }
    public virtual DbSet<TblDoorstepApplication> TblDoorstepApplications { get; set; }

    public virtual DbSet<TblDoorstepReqDtl> TblDoorstepReqDtls { get; set; }
    public virtual DbSet<TblDoorstepLogin> TblDoorstepLogins { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var A4 = System.Configuration.ConfigurationManager.AppSettings["doorstep"];
        // var encryptedText = EncryptPlainTextToCipherText(A4);
        var decryptedText = DecryptCipherTextToPlainText(A4);
        //    Console.WriteLine("Passed Text = " + A4);
        //    Console.WriteLine("EncryptedText = " + encryptedText);
        //   Console.WriteLine("@\"" + decryptedText + "\"");

        optionsBuilder.UseOracle("@\"" + decryptedText + "\"");
        optionsBuilder.UseOracle(decryptedText);
        optionsBuilder.EnableSensitiveDataLogging();

    }

    private const string SecurityKey = "ComplexKeyHere_12121";
    public string EncryptPlainTextToCipherText(string PlainText)

    {
        byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

        MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

        byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));

        objMD5CryptoService.Clear();

        var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

        objTripleDESCryptoService.Key = securityKeyArray;

        objTripleDESCryptoService.Mode = CipherMode.ECB;

        objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


        var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();

        byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
        objTripleDESCryptoService.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }
    public string DecryptCipherTextToPlainText(string CipherText)
    {
        byte[] toEncryptArray = Convert.FromBase64String(CipherText);
        MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();


        byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
        objMD5CryptoService.Clear();

        var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

        objTripleDESCryptoService.Key = securityKeyArray;

        objTripleDESCryptoService.Mode = CipherMode.ECB;

        objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

        var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();

        byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        objTripleDESCryptoService.Clear();


        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("GL_UAT")
            .UseCollation("USING_NLS_COMP");



        modelBuilder.Entity<TblDoorstepLogin>(entity =>
        {
            entity.HasKey(e => new { e.RefId, e.EmpCode });

            entity.ToTable("TBL_DOORSTEP_LOGIN");

            entity.Property(e => e.EmpCode)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.LoginTime)
                .HasColumnType("DATE")
                .HasColumnName("LOGIN_TIME");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("STATUS");
            entity.Property(e => e.ComputerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COMPUTER_NAME");
            entity.Property(e => e.LogoutTime)
                .HasColumnType("DATE")
                .HasColumnName("LOGOUT_TIME");
            entity.Property(e => e.MacAddress)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("MAC_ADDRESS");
            entity.Property(e => e.PrivateIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PRIVATE_IP");
            entity.Property(e => e.PublicIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PUBLIC_IP");
            entity.Property(e => e.RefId)
                .HasPrecision(6)
                .HasColumnName("REF_ID");
            //entity.Property(e => e.WifiName)
            //    .HasMaxLength(50)
            //    .IsUnicode(false)
            //    .HasColumnName("WIFI_NAME");
        });


        modelBuilder.Entity<BranchMaster>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("P_BRANCH_MASTER");

            entity.ToTable("BRANCH_MASTER");

            entity.HasIndex(e => e.FirmId, "I_BRANCH_MASTER_1");

            entity.HasIndex(e => e.StateId, "I_BRANCH_MASTER_2");

            entity.Property(e => e.BranchId)
                .HasPrecision(5)
                .ValueGeneratedNever()
                .HasColumnName("BRANCH_ID");
            entity.Property(e => e.BranchAbbr)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ABBR");
            entity.Property(e => e.BranchAdd1)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ADD1");
            entity.Property(e => e.BranchAdd2)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ADD2");
            entity.Property(e => e.BranchAdd3)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ADD3");
            entity.Property(e => e.BranchAdd4)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ADD4");
            entity.Property(e => e.BranchAdd5)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ADD5");
            entity.Property(e => e.BranchAddr)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("BRANCH_ADDR");
            entity.Property(e => e.BranchCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("BRANCH_CODE");
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BRANCH_NAME");
            entity.Property(e => e.BranchNo)
                .HasPrecision(5)
                .HasColumnName("BRANCH_NO");
            entity.Property(e => e.DistrictId)
                .HasPrecision(5)
                .HasColumnName("DISTRICT_ID");
            entity.Property(e => e.FirmId)
                .HasPrecision(3)
                .HasColumnName("FIRM_ID");
            entity.Property(e => e.InaugurationDt)
                .HasColumnType("DATE")
                .HasColumnName("INAUGURATION_DT");
            entity.Property(e => e.IntWaiverApprd)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(1)")
                .HasColumnName("INT_WAIVER_APPRD");
            entity.Property(e => e.LocalBody)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("LOCAL_BODY");
            entity.Property(e => e.Phone1)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE1");
            entity.Property(e => e.Phone2)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PHONE2");
            entity.Property(e => e.Pincode)
                .HasPrecision(7)
                .HasColumnName("PINCODE");
            entity.Property(e => e.RegionId)
                .HasPrecision(2)
                .HasColumnName("REGION_ID");
            entity.Property(e => e.StateId)
                .HasPrecision(3)
                .HasColumnName("STATE_ID");
            entity.Property(e => e.StatusId)
                .HasPrecision(2)
                .HasColumnName("STATUS_ID");
            entity.Property(e => e.TraDt)
                .HasColumnType("DATE")
                .HasColumnName("TRA_DT");
            entity.Property(e => e.UptoDate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("UPTO_DATE");
        });

        modelBuilder.Entity<BlockedDevice>(entity =>
        {
            entity
                .HasKey(e => e.DeviceId);
            entity.ToTable("BLOCKED_DEVICES");

            entity.Property(e => e.ActiveStatus)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("ACTIVE_STATUS");
            entity.Property(e => e.Attempt)
                .HasPrecision(2)
                .HasColumnName("ATTEMPT");
            entity.Property(e => e.DeviceId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEVICE_ID");
            entity.Property(e => e.LastAttemptDate)
                .HasColumnType("DATE")
                .HasColumnName("LAST_ATTEMPT_DATE");
        });
        modelBuilder.Entity<Dual>(entity =>
        {
            entity.HasKey(e => e.SysDate);
            //await _dbContext.Dual.FromSqlRaw("Select sysdate from dual;").ToListAsync();
        });
        modelBuilder.Entity<DistrictMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DISTRICT_MASTER");

            entity.HasIndex(e => e.StateId, "I_DISTRICT_1");

            entity.HasIndex(e => e.DistrictId, "P_DISTRICT_ID").IsUnique();

            entity.Property(e => e.DistrictId)
                .HasPrecision(5)
                .HasColumnName("DISTRICT_ID");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("DISTRICT_NAME");
            entity.Property(e => e.StateId)
                .HasPrecision(2)
                .HasColumnName("STATE_ID");
        });

        modelBuilder.Entity<TblDoorstepConfiguration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TBL_DOORSTEP_CONFIGURATION");

            entity.Property(e => e.ApiHitcount)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("API_HITCOUNT");
            entity.Property(e => e.MaximumGram)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("MAXIMUM_GRAM");
            entity.Property(e => e.MaximumScheduletime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAXIMUM_SCHEDULETIME");
            entity.Property(e => e.MaximumWithdrawalamt)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("MAXIMUM_WITHDRAWALAMT");
            entity.Property(e => e.MinimumGram)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("MINIMUM_GRAM");
            entity.Property(e => e.MinimumScheduletime)
               .HasMaxLength(50)
               .IsUnicode(false)
               .HasColumnName("MINIMUM_SCHEDULETIME");
            entity.Property(e => e.MinimumWithdrawamt)
                .HasColumnType("NUMBER(12,2)")
                .HasColumnName("MINIMUM_WITHDRAWAMT");
            entity.Property(e => e.OtpblockTime)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("OTPBLOCK_TIME");
            entity.Property(e => e.SecuritycodeMaxtime)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SECURITYCODE_MAXTIME");
            entity.Property(e => e.PendingReqLimit)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PENDING_REQ_LIMIT");

        });
        modelBuilder.Entity<TblDoorstepApplication>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TBL_DOORSTEP_APPLICATIONS");

            entity.Property(e => e.AppNo)
                .HasPrecision(6)
                .HasColumnName("APP_NO");
            entity.Property(e => e.BuildDate)
                .HasColumnType("DATE")
                .HasColumnName("BUILD_DATE");
            entity.Property(e => e.Builder)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BUILDER");
            entity.Property(e => e.FirmId)
                .HasPrecision(2)
                .HasColumnName("FIRM_ID");
            entity.Property(e => e.ModuleId)
                .HasPrecision(2)
                .HasColumnName("MODULE_ID");
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USER_TYPE");
            entity.Property(e => e.VersionNo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("VERSION_NO");
        });
        //modelBuilder.Entity<TblDoorstepConfiguration>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToTable("TBL_DOORSTEP_CONFIGURATION");

        //    entity.Property(e => e.MaximumGram)
        //        .HasColumnType("NUMBER(38)")
        //        .HasColumnName("MAXIMUM_GRAM");
        //    entity.Property(e => e.MaximumScheduletime)
        //        .HasMaxLength(50)
        //        .IsUnicode(false)
        //        .HasColumnName("MAXIMUM_SCHEDULETIME");
        //    entity.Property(e => e.MaximumWithdrawalamt)
        //        .HasColumnType("NUMBER(38)")
        //        .HasColumnName("MAXIMUM_WITHDRAWALAMT");
        //    entity.Property(e => e.MinimumGram)
        //        .HasColumnType("NUMBER(38)")
        //        .HasColumnName("MINIMUM_GRAM");
        //    entity.Property(e => e.MinimumWithdrawamt)
        //        .HasColumnType("NUMBER(38)")
        //        .HasColumnName("MINIMUM_WITHDRAWAMT");
        //    entity.Property(e => e.OtpblockTime)
        //        .HasColumnType("NUMBER(38)")
        //        .HasColumnName("OTPBLOCK_TIME");
        //});

        modelBuilder.Entity<EmployeeMaster>(entity =>
        {
            entity.HasKey(e => e.EmpCode).HasName("P_EMPLOYEE_MASTER");

            entity.ToTable("EMPLOYEE_MASTER");

            entity.HasIndex(e => new { e.StatusId, e.EmpCode }, "IDX$$_00010041");

            entity.HasIndex(e => new { e.EmpCode, e.StatusId }, "IDX$$_00010047");

            entity.HasIndex(e => new { e.EmpCode, e.FirmId, e.DepartmentId, e.DesignationId, e.PostId }, "I_EMPLOYEE_MASTER1");

            entity.HasIndex(e => e.BranchId, "I_EMPLOYEE_MASTER_10");

            entity.HasIndex(e => e.DesignationId, "I_EMPLOYEE_MASTER_5");

            entity.HasIndex(e => e.DepartmentId, "I_EMPLOYEE_MASTER_7");

            entity.HasIndex(e => e.StatusId, "I_EMPLOYEE_MASTER_9");

            entity.Property(e => e.EmpCode)
                .HasPrecision(6)
                .ValueGeneratedNever()
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.AccessId)
                .HasPrecision(4)
                .HasColumnName("ACCESS_ID");
            entity.Property(e => e.BasicDt)
                .HasColumnType("DATE")
                .HasColumnName("BASIC_DT");
            entity.Property(e => e.BasicPay)
                .HasColumnType("NUMBER(9,2)")
                .HasColumnName("BASIC_PAY");
            entity.Property(e => e.BlockId)
                .HasPrecision(3)
                .HasColumnName("BLOCK_ID");
            entity.Property(e => e.BondFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BOND_FLAG");
            entity.Property(e => e.BranchId)
                .HasPrecision(4)
                .HasColumnName("BRANCH_ID");
            entity.Property(e => e.Category)
                .HasPrecision(3)
                .HasColumnName("CATEGORY");
            entity.Property(e => e.DaFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("DA_FLAG");
            entity.Property(e => e.DepartmentId)
                .HasPrecision(4)
                .HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.DesignationId)
                .HasPrecision(3)
                .HasColumnName("DESIGNATION_ID");
            entity.Property(e => e.EmpName)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("EMP_NAME");
            entity.Property(e => e.EmpType)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("EMP_TYPE");
            entity.Property(e => e.EsiFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ESI_FLAG");
            entity.Property(e => e.FirmId)
                .HasPrecision(2)
                .HasColumnName("FIRM_ID");
            entity.Property(e => e.GradeId)
                .HasPrecision(2)
                .HasColumnName("GRADE_ID");
            entity.Property(e => e.GratiFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("GRATI_FLAG");
            entity.Property(e => e.JoinDt)
                .HasColumnType("DATE")
                .HasColumnName("JOIN_DT");
            entity.Property(e => e.MediclaimFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MEDICLAIM_FLAG");
            entity.Property(e => e.PaidAmt)
                .HasColumnType("NUMBER(7,2)")
                .HasColumnName("PAID_AMT");
            entity.Property(e => e.Password).HasColumnName("PASSWORD");
            entity.Property(e => e.PfFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PF_FLAG");
            entity.Property(e => e.PostId)
                .HasPrecision(4)
                .HasColumnName("POST_ID");
            entity.Property(e => e.Rejoining)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("REJOINING");
            entity.Property(e => e.SecurityDep)
                .HasColumnType("NUMBER(7,2)")
                .HasColumnName("SECURITY_DEP");
            entity.Property(e => e.ShiftId)
                .HasPrecision(3)
                .HasColumnName("SHIFT_ID");
            entity.Property(e => e.StampPaper)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("STAMP_PAPER");
            entity.Property(e => e.StatusId)
                .HasPrecision(2)
                .HasColumnName("STATUS_ID");
            entity.Property(e => e.TaxFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TAX_FLAG");
        });

        modelBuilder.Entity<KeyMaster>(entity =>
        {
            entity.HasKey(e => new { e.FirmId, e.BranchId, e.ModuleId, e.KeyId }).HasName("P_KEY_MASTER");

            entity.ToTable("KEY_MASTER");

            entity.Property(e => e.FirmId)
                .HasPrecision(3)
                .HasColumnName("FIRM_ID");
            entity.Property(e => e.BranchId)
                .HasPrecision(4)
                .HasColumnName("BRANCH_ID");
            entity.Property(e => e.ModuleId)
                .HasPrecision(3)
                .HasColumnName("MODULE_ID");
            entity.Property(e => e.KeyId)
                .HasPrecision(10)
                .HasColumnName("KEY_ID");
            entity.Property(e => e.Description)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Value)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("VALUE");
        });

        modelBuilder.Entity<PostMaster>(entity =>
        {
            entity.HasKey(e => e.SrNumber).HasName("P_POST_MASTER");

            entity.ToTable("POST_MASTER");

            entity.HasIndex(e => e.DistrictId, "I_POST_MASTER");

            entity.Property(e => e.SrNumber)
                .HasPrecision(7)
                .ValueGeneratedNever()
                .HasColumnName("SR_NUMBER");
            entity.Property(e => e.DistrictId)
                .HasPrecision(5)
                .HasColumnName("DISTRICT_ID");
            entity.Property(e => e.PinCode)
                .HasPrecision(6)
                .HasColumnName("PIN_CODE");
            entity.Property(e => e.PostOffice)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("POST_OFFICE");
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("STATE_MASTER");

            entity.HasIndex(e => e.StateId, "IDX$$_00010013");

            entity.Property(e => e.CountryId)
                .HasPrecision(5)
                .HasColumnName("COUNTRY_ID");
            entity.Property(e => e.StateAbbr)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("STATE_ABBR");
            entity.Property(e => e.StateCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("NULL")
                .IsFixedLength()
                .HasColumnName("STATE_CODE");
            entity.Property(e => e.StateId)
                .HasPrecision(2)
                .HasColumnName("STATE_ID");
            entity.Property(e => e.StateName)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("STATE_NAME");
        });
        modelBuilder.Entity<PledgeScheme>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PLEDGE_SCHEME");

            entity.HasIndex(e => new { e.FirmId, e.FromDt, e.ToDt, e.SchemeNm }, "I_PLEDGE_SCHEME");

            entity.HasIndex(e => new { e.BranchId, e.FirmId, e.FromDt, e.ToDt }, "I_PLSCH");

            entity.Property(e => e.AdvChrg)
                .HasColumnType("NUMBER(6,2)")
                .HasColumnName("ADV_CHRG");
            entity.Property(e => e.AnnualIntRate)
                .HasDefaultValueSql("0")
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("ANNUAL_INT_RATE");
            entity.Property(e => e.AppRate)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("APP_RATE");
            entity.Property(e => e.BranchId)
                .HasPrecision(5)
                .HasColumnName("BRANCH_ID");
            entity.Property(e => e.ChangeId)
                .HasPrecision(5)
                .HasColumnName("CHANGE_ID");
            entity.Property(e => e.DeductLtv)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("DEDUCT_LTV");
            entity.Property(e => e.FirmId)
                .HasPrecision(2)
                .HasColumnName("FIRM_ID");
            entity.Property(e => e.FromDt)
                .HasColumnType("DATE")
                .HasColumnName("FROM_DT");
            entity.Property(e => e.IntRate)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("INT_RATE");
            entity.Property(e => e.LndRate)
                .HasColumnType("NUMBER(7,2)")
                .HasColumnName("LND_RATE");
            entity.Property(e => e.LtvPercent)
                .HasPrecision(3)
                .HasColumnName("LTV_PERCENT");
            entity.Property(e => e.MarketRate)
                .HasPrecision(5)
                .HasColumnName("MARKET_RATE");
            entity.Property(e => e.MaxBal)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("MAX_BAL");
            entity.Property(e => e.MaxLoan)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("MAX_LOAN");
            entity.Property(e => e.MinLoan)
                .HasColumnType("NUMBER(10,2)")
                .HasColumnName("MIN_LOAN");
            entity.Property(e => e.OvrDue)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("OVR_DUE");
            entity.Property(e => e.Period)
                .HasPrecision(3)
                .HasColumnName("PERIOD");
            entity.Property(e => e.PostChrg)
                .HasColumnType("NUMBER(6,2)")
                .HasColumnName("POST_CHRG");
            entity.Property(e => e.SchemeId)
                .HasPrecision(2)
                .HasColumnName("SCHEME_ID");
            entity.Property(e => e.SchemeName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("SCHEME_NAME");
            entity.Property(e => e.SchemeNm)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SCHEME_NM");
            entity.Property(e => e.SerRate)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("SER_RATE");
            entity.Property(e => e.ToDt)
                .HasColumnType("DATE")
                .HasColumnName("TO_DT");
            entity.Property(e => e.UpfrontInt)
                .HasColumnType("NUMBER(5,2)")
                .HasColumnName("UPFRONT_INT");
        });

        modelBuilder.Entity<TblDoorstepCustMst>(entity =>
        {
            entity
                 .HasKey(e => new { e.CustId, e.MobNo });
               // .HasNoKey();
            entity.ToTable("TBL_DOORSTEP_CUST_MST");

            entity.Property(e => e.CustId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CUST_ID");
            entity.Property(e => e.CustLang)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CUST_LANG");
            entity.Property(e => e.CustName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CUST_NAME");
            entity.Property(e => e.Doorkey)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("DOORKEY");
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EMAIL_ID");
            entity.Property(e => e.MobNo)
                .HasPrecision(10)
                .HasColumnName("MOB_NO");
            entity.Property(e => e.TraDt)
                .HasColumnType("DATE")
                .HasColumnName("TRA_DT");
        });
        modelBuilder.Entity<DoorstepSplash>(entity =>
        {
            entity.HasKey(e => new { e.DeviceId, e.EntryTime }).HasName("SYS_C0033221");

            entity.ToTable("DOORSTEP_SPLASH");

            entity.Property(e => e.DeviceId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DEVICE_ID");
            entity.Property(e => e.DeviceDetail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEVICE_DETAIL");
            entity.Property(e => e.EntryTime)
                .HasColumnType("DATE")
                .HasColumnName("ENTRY_TIME");
            entity.Property(e => e.ModeCategory)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MODE_CATEGORY");
        });
        modelBuilder.Entity<TblDoorstepOtpLog>(entity =>
        {

            entity.HasKey(e => new { e.TransId })
                .HasName("PK_DOORSTEP_OTP_LOG");

            entity.ToTable("TBL_DOORSTEP_OTP_LOG");

            entity.Property(e => e.HitCount)
                .HasPrecision(5)
                .HasColumnName("HIT_COUNT");
            entity.Property(e => e.MobNo)
                .HasPrecision(12)
                .HasColumnName("MOB_NO");
            entity.Property(e => e.Otp)
                .HasPrecision(7)
                .HasColumnName("OTP");
            entity.Property(e => e.OtpStatus)
                .HasPrecision(2)
                .HasColumnName("OTP_STATUS");
            entity.Property(e => e.TraDate)
                .HasColumnType("DATE")
                .HasColumnName("TRA_DATE");
            entity.Property(e => e.TransId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TRANS_ID");
        });
        modelBuilder.Entity<HrmTourDtl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("HRM_TOUR_DTL");

            entity.HasIndex(e => new { e.EmpCode, e.FromDt, e.ToDt, e.BranchId, e.PostId, e.DepId, e.TourId }, "TOUR1");

            entity.HasIndex(e => new { e.EmpCode, e.FromDt, e.ToDt, e.TourId }, "TOUR2");

            entity.Property(e => e.AdvanceRs)
                .HasColumnType("NUMBER(15,2)")
                .HasColumnName("ADVANCE_RS");
            entity.Property(e => e.BranchId)
                .HasPrecision(4)
                .HasColumnName("BRANCH_ID");
            entity.Property(e => e.DepId)
                .HasPrecision(5)
                .HasColumnName("DEP_ID");
            entity.Property(e => e.DesigId)
                .HasPrecision(3)
                .HasColumnName("DESIG_ID");
            entity.Property(e => e.EmpCode)
                .HasPrecision(6)
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.FromDt)
                .HasColumnType("DATE")
                .HasColumnName("FROM_DT");
            entity.Property(e => e.FromTime)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("FROM_TIME");
            entity.Property(e => e.Others)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OTHERS");
            entity.Property(e => e.PostId)
                .HasPrecision(4)
                .HasColumnName("POST_ID");
            entity.Property(e => e.RecomDt)
                .HasColumnType("DATE")
                .HasColumnName("RECOM_DT");
            entity.Property(e => e.RecomPerson)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RECOM_PERSON");
            entity.Property(e => e.RejectDt)
                .HasColumnType("DATE")
                .HasColumnName("REJECT_DT");
            entity.Property(e => e.RejectReason)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("REJECT_REASON");
            entity.Property(e => e.SanctionDt)
                .HasColumnType("DATE")
                .HasColumnName("SANCTION_DT");
            entity.Property(e => e.SanctionPerson)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SANCTION_PERSON");
            entity.Property(e => e.SrNumber)
                .HasPrecision(9)
                .HasColumnName("SR_NUMBER");
            entity.Property(e => e.ToBranch)
                .HasPrecision(4)
                .HasColumnName("TO_BRANCH");
            entity.Property(e => e.ToDt)
                .HasColumnType("DATE")
                .HasColumnName("TO_DT");
            entity.Property(e => e.ToTime)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("TO_TIME");
            entity.Property(e => e.TourId)
                .HasPrecision(2)
                .HasColumnName("TOUR_ID");
            entity.Property(e => e.TourPurpose)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("TOUR_PURPOSE");
            entity.Property(e => e.TraDt)
                .HasColumnType("DATE")
                .HasColumnName("TRA_DT");
            entity.Property(e => e.TrainingNormal)
                .HasColumnType("NUMBER(1)")
                .HasColumnName("TRAINING_NORMAL");
        });

        modelBuilder.Entity<TblDoorstepReqDtl>(entity =>
        {
            entity
                .HasKey(e =>new { e.CustomerId, e.ReqId })
                .HasName("PK_CUSTOMERID");
            entity.ToTable("TBL_DOORSTEP_REQ_DTL");

            entity.Property(e => e.Address1)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Address2)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.Amount)
                .HasPrecision(15)
                .HasColumnName("AMOUNT");
            entity.Property(e => e.AssLatitude)
                .HasColumnType("NUMBER(10,5)")
                .HasColumnName("ASS_LATITUDE");
            entity.Property(e => e.AssLongitude)
                .HasColumnType("NUMBER(10,5)")
                .HasColumnName("ASS_LONGITUDE");
            entity.Property(e => e.AssignEmp)
                .HasPrecision(10)
                .HasColumnName("ASSIGN_EMP");
            entity.Property(e => e.AssignedDate)
                .HasColumnType("DATE")
                .HasColumnName("ASSIGNED_DATE");
            entity.Property(e => e.Branchid)
                .HasPrecision(4)
                .HasColumnName("BRANCHID");
            entity.Property(e => e.Comments)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("COMMENTS");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.DistId)
                .HasPrecision(4)
                .HasColumnName("DIST_ID");
            entity.Property(e => e.Doorstepkey)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DOORSTEPKEY");
            entity.Property(e => e.Empcode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EMPCODE");
            entity.Property(e => e.GlDisDt)
                .HasColumnType("DATE")
                .HasColumnName("GL_DIS_DT");
            entity.Property(e => e.GrossWt)
                .HasColumnType("NUMBER(7,3)")
                .HasColumnName("GROSS_WT");
            entity.Property(e => e.Latitude)
                .HasColumnType("NUMBER(10,5)")
                .HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude)
                .HasColumnType("NUMBER(10,5)")
                .HasColumnName("LONGITUDE");
            entity.Property(e => e.PinCode)
                .HasPrecision(6)
                .HasColumnName("PIN_CODE");
            entity.Property(e => e.ReqEmp)
                .HasPrecision(10)
                .HasColumnName("REQ_EMP");
            entity.Property(e => e.ReqId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("REQ_ID");
            entity.Property(e => e.ReqStatus)
                .HasPrecision(2)
                .HasColumnName("REQ_STATUS");
            entity.Property(e => e.ScheduleTime)
                .HasColumnType("DATE")
                .HasColumnName("SCHEDULE_TIME");
            entity.Property(e => e.SecCode)
                .HasPrecision(6)
                .HasColumnName("SEC_CODE");
            entity.Property(e => e.StateId)
                .HasPrecision(4)
                .HasColumnName("STATE_ID");
            entity.Property(e => e.SysmupdDate)
                .HasColumnType("DATE")
                .HasColumnName("SYSMUPD_DATE");
            entity.Property(e => e.TakeoverStatus)
                .HasPrecision(1)
                .HasColumnName("TAKEOVER_STATUS");
            entity.Property(e => e.TakeoverType)
                .HasPrecision(10)
                .HasColumnName("TAKEOVER_TYPE");
            entity.Property(e => e.TraDt)
                .HasColumnType("DATE")
                .HasColumnName("TRA_DT");
            entity.Property(e => e.UpdCode)
                .HasPrecision(10)
                .HasColumnName("UPD_CODE");
        });
        modelBuilder.HasSequence("ACC_SEQ").IsCyclic();
        modelBuilder.HasSequence("CIRCULAR_SEQ").IsCyclic();
        modelBuilder.HasSequence("CLIENT_SEQ");
        modelBuilder.HasSequence("COLL_SEQ").IsCyclic();
        modelBuilder.HasSequence("CUST_CODE");
        modelBuilder.HasSequence("DEBALLOTNO_SEQ");
        modelBuilder.HasSequence("DOOSTEP_CUSTID");
        modelBuilder.HasSequence("DOOSTEP_REQID");
        modelBuilder.HasSequence("EMAIL_SEQ");
        modelBuilder.HasSequence("J_LEAVE");
        modelBuilder.HasSequence("MAFOUND_ACC_ENTRY_TEMP_ID");
        modelBuilder.HasSequence("MAFOUND_ACC_TRAN_UNIQUE_ID");
        modelBuilder.HasSequence("MAFOUND_API_EXCEPTION_ID");
        modelBuilder.HasSequence("MAFOUND_API_REQ_LOG_ID");
        modelBuilder.HasSequence("MAFOUND_API_USER_ID");
        modelBuilder.HasSequence("MAIL_CALENDAR_SEQUENCE").IsCyclic();
        modelBuilder.HasSequence("MAIL_SEQ").IsCyclic();
        modelBuilder.HasSequence("MAIL_SEQ_TEMP").IsCyclic();
        modelBuilder.HasSequence("MAIL_SEQUENCE").IsCyclic();
        modelBuilder.HasSequence("MICROSOFTSEQDTPROPERTIES");
        modelBuilder.HasSequence("ONE_SEQ");
        modelBuilder.HasSequence("ORACLESEQCOUNT");
        modelBuilder.HasSequence("PLEDGE_SEQ").IsCyclic();
        modelBuilder.HasSequence("PLSQL_PROFILER_RUNNUMBER");
        modelBuilder.HasSequence("PRECLOSURESERIES");
        modelBuilder.HasSequence("QA_DEFFECT_IDS");
        modelBuilder.HasSequence("QA_MEDIAFILEID_SEQ");
        modelBuilder.HasSequence("REGISTER_SEQ");
        modelBuilder.HasSequence("RENT_MABEN");
        modelBuilder.HasSequence("RENT_TDS_CERTIFICATE").IsCyclic();
        modelBuilder.HasSequence("REP_SEQ").IsCyclic();
        modelBuilder.HasSequence("SECURITY_MEM").IsCyclic();
        modelBuilder.HasSequence("SEQ_ACC1");
        modelBuilder.HasSequence("SEQ_BAL1");
        modelBuilder.HasSequence("SEQ_BUG_LOG_ID");
        modelBuilder.HasSequence("SEQ_BUG_TRACK_ID");
        modelBuilder.HasSequence("SEQ_BUG_TRACKING");
        modelBuilder.HasSequence("SEQ_CASH_P");
        modelBuilder.HasSequence("SEQ_CASH1");
        modelBuilder.HasSequence("SEQ_CRB_APPID");
        modelBuilder.HasSequence("SEQ_CRB_ID");
        modelBuilder.HasSequence("SEQ_CRB_RECCOMID");
        modelBuilder.HasSequence("SEQ_D_A");
        modelBuilder.HasSequence("SEQ_DAILY_WORK_TRACK");
        modelBuilder.HasSequence("SEQ_DELAY_ID");
        modelBuilder.HasSequence("SEQ_FEEDBACK_ID");
        modelBuilder.HasSequence("SEQ_FUND");
        modelBuilder.HasSequence("SEQ_INSU_COM_ID");
        modelBuilder.HasSequence("SEQ_INV");
        modelBuilder.HasSequence("SEQ_IP_ID");
        modelBuilder.HasSequence("SEQ_ITEM_ID");
        modelBuilder.HasSequence("SEQ_ITPROJECT_ERRORID");
        modelBuilder.HasSequence("SEQ_NEW_FA");
        modelBuilder.HasSequence("SEQ_POLU_ID");
        modelBuilder.HasSequence("SEQ_PROJECT_ID");
        modelBuilder.HasSequence("SEQ_PWA_ID");
        modelBuilder.HasSequence("SEQ_REF1");
        modelBuilder.HasSequence("SEQ_RENT_AGREE");
        modelBuilder.HasSequence("SEQ_RSL_NPA_VIG_ID");
        modelBuilder.HasSequence("SEQ_SERVICE_DOC");
        modelBuilder.HasSequence("SEQ_SERVICE_ID");
        modelBuilder.HasSequence("SEQ_SUB_TASK");
        modelBuilder.HasSequence("SEQ_TAX").IsCyclic();
        modelBuilder.HasSequence("SEQ_TAX_NOTICE");
        modelBuilder.HasSequence("SEQ_TEMPID");
        modelBuilder.HasSequence("SEQ_TRACKER_ID");
        modelBuilder.HasSequence("SEQ_TRANSACTION_ID");
        modelBuilder.HasSequence("SEQ_TSK");
        modelBuilder.HasSequence("SEQ_UNIQ_ID");
        modelBuilder.HasSequence("SEQ_VIGILANCE");
        modelBuilder.HasSequence("SEQ_VIGILANCE_SR_NO");
        modelBuilder.HasSequence("SEQ_VIGILANCE_VISIT");
        modelBuilder.HasSequence("SEQ1").IsCyclic();
        modelBuilder.HasSequence("TDS_CERTIFICATE").IsCyclic();
        modelBuilder.HasSequence("TEMP_SEQ");
        modelBuilder.HasSequence("TESTSEQ");
        modelBuilder.HasSequence("TRUSTEESERIES");
        modelBuilder.HasSequence("VAPT_ASSIGNED_ID_SEQ");
        modelBuilder.HasSequence("VAPT_ISSUE_ID_SEQ");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
