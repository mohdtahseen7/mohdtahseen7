using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GOLDLOAN.ModelClass
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        string connectionstring = Environment.GetEnvironmentVariable("GLCONNECTION_STRING");

        public virtual DbSet<BranchDetail> BranchDetails { get; set; } = null!;
        public virtual DbSet<BranchMaster> BranchMasters { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<EmpMaster> EmpMasters { get; set; } = null!;
        public virtual DbSet<EmployFirm> EmployFirms { get; set; } = null!;
        public virtual DbSet<EmployeeMaster> EmployeeMasters { get; set; } = null!;
        public virtual DbSet<GeneralParameter> GeneralParameters { get; set; } = null!;
        public virtual DbSet<ModeOfTransfer> ModeOfTransfers { get; set; } = null!;
        public virtual DbSet<ModeOfTranspost> ModeOfTransposts { get; set; } = null!;
        public virtual DbSet<NeftBankMst> NeftBankMsts { get; set; } = null!;
        public virtual DbSet<NeftCustomer> NeftCustomers { get; set; } = null!;
        public virtual DbSet<PledgeMs189> PledgeMs189s { get; set; } = null!;
        public virtual DbSet<SubsidaryMaster> SubsidaryMasters { get; set; } = null!;
        public virtual DbSet<TakeoverConfirmationDtl> TakeoverConfirmationDtls { get; set; } = null!;
        public virtual DbSet<TakeoverloanDtl> TakeoverloanDtls { get; set; } = null!;
        public virtual DbSet<TakeoverloanMst> TakeoverloanMsts { get; set; } = null!;
        public virtual DbSet<Takeoverloanquotreq> Takeoverloanquotreqs { get; set; } = null!;
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; } = null!;
        public virtual DbSet<VehicleName> VehicleNames { get; set; } = null!;
        public virtual DbSet<Dual> Duals { get; set; } = null!;
        public virtual DbSet<KeyMaster> KeyMasters { get; set; } = null!;


        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("User Id=gl_uat;Password=pass#1234;Data Source=10.192.5.76:1521/macuatpdb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("GL_UAT");


            modelBuilder.Entity<KeyMaster>(entity =>
            {
                entity.HasKey(e => new { e.FirmId, e.BranchId, e.ModuleId, e.KeyId })
                    .HasName("P_KEY_MASTER");

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

            modelBuilder.Entity<BranchDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("BRANCH_DETAIL");

                entity.Property(e => e.AreaHead)
                    .HasMaxLength(81)
                    .IsUnicode(false)
                    .HasColumnName("AREA_HEAD");

                entity.Property(e => e.AreaId)
                    .HasPrecision(3)
                    .HasColumnName("AREA_ID");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("AREA_NAME");

                entity.Property(e => e.BranchId)
                    .HasPrecision(5)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH_NAME");

                entity.Property(e => e.DistrictId)
                    .HasPrecision(5)
                    .HasColumnName("DISTRICT_ID");

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DISTRICT_NAME");

                entity.Property(e => e.DivHead)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DIV_HEAD");

                entity.Property(e => e.DivName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("DIV_NAME");

                entity.Property(e => e.DivisionId)
                    .HasPrecision(3)
                    .HasColumnName("DIVISION_ID");

                entity.Property(e => e.FirmId)
                    .HasPrecision(3)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.InaugurationDt)
                    .HasColumnType("DATE")
                    .HasColumnName("INAUGURATION_DT");

                entity.Property(e => e.RegHead)
                    .HasMaxLength(81)
                    .IsUnicode(false)
                    .HasColumnName("REG_HEAD");

                entity.Property(e => e.RegId)
                    .HasPrecision(4)
                    .HasColumnName("REG_ID");

                entity.Property(e => e.RegName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("REG_NAME");

                entity.Property(e => e.StateId)
                    .HasPrecision(3)
                    .HasColumnName("STATE_ID");

                entity.Property(e => e.StateName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STATE_NAME");

                entity.Property(e => e.StatusId)
                    .HasPrecision(2)
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.ZonalHead)
                    .HasMaxLength(141)
                    .IsUnicode(false)
                    .HasColumnName("ZONAL_HEAD");

                entity.Property(e => e.ZonalId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ZONAL_ID");

                entity.Property(e => e.ZonalName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ZONAL_NAME");
            });

            modelBuilder.Entity<BranchMaster>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("P_BRANCH_MASTER");

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
                    .HasPrecision(1)
                    .HasColumnName("INT_WAIVER_APPRD")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LocalBody)
                    .HasPrecision(1)
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
                    .HasColumnName("UPTO_DATE")
                    .IsFixedLength();
            });
            modelBuilder.Entity<Dual>(entity =>
            {
                entity.HasKey(e => e.SysDate);
                //await _dbContext.Dual.FromSqlRaw("Select sysdate from dual;").ToListAsync();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustId)
                    .HasName("P_CUSTOMER");

                entity.ToTable("CUSTOMER");

                entity.HasIndex(e => e.BranchId, "I_CUSTOMER");

                entity.HasIndex(e => e.FirmId, "I_CUSTOMER1");

                entity.HasIndex(e => e.PinSerial, "I_CUSTOMER_2");

                entity.HasIndex(e => e.CustName, "I_CUST_NAME");

                entity.HasIndex(e => e.Name, "N_CUSTOMER");

                entity.HasIndex(e => e.Phone1, "P_PHONE1");

                entity.HasIndex(e => e.Phone2, "P_PHONE2");

                entity.Property(e => e.CustId)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("CUST_ID");

                entity.Property(e => e.AltHouseName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ALT_HOUSE_NAME");

                entity.Property(e => e.AltLocality)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ALT_LOCALITY");

                entity.Property(e => e.AltPinSerial)
                    .HasPrecision(7)
                    .HasColumnName("ALT_PIN_SERIAL");

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.CardNo)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("CARD_NO");

                entity.Property(e => e.CountryId)
                    .HasPrecision(4)
                    .HasColumnName("COUNTRY_ID");

                entity.Property(e => e.CustName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("CUST_NAME");

                entity.Property(e => e.FatHus)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("FAT_HUS");

                entity.Property(e => e.FatHusPre)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FAT_HUS_PRE");

                entity.Property(e => e.FirmId)
                    .HasPrecision(3)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.ForsFlag)
                    .HasColumnType("NUMBER")
                    .HasColumnName("FORS_FLAG");

                entity.Property(e => e.HouseName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("HOUSE_NAME");

                entity.Property(e => e.LandMark)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("LAND_MARK");

                entity.Property(e => e.LastModiDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LAST_MODI_DATE");

                entity.Property(e => e.LeadNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LEAD_NUMBER");

                entity.Property(e => e.Locality)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("LOCALITY");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("MARITAL_STATUS")
                    .IsFixedLength();

                entity.Property(e => e.MothersName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("MOTHERS_NAME");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.NamePre)
                    .HasPrecision(1)
                    .HasColumnName("NAME_PRE");

                entity.Property(e => e.PhonModStatus)
                    .HasPrecision(5)
                    .HasColumnName("PHON_MOD_STATUS")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Phone1)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE1");

                entity.Property(e => e.Phone2)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PHONE2");

                entity.Property(e => e.Phone3)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONE3");

                entity.Property(e => e.PinSerial)
                    .HasPrecision(7)
                    .HasColumnName("PIN_SERIAL");

                entity.Property(e => e.PsdNo)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("PSD_NO");

                entity.Property(e => e.RefMobNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("REF_MOB_NO");

                entity.Property(e => e.SancationBy)
                    .HasPrecision(6)
                    .HasColumnName("SANCATION_BY");

                entity.Property(e => e.ShareNo)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("SHARE_NO");

                entity.Property(e => e.Sharecount)
                    .HasPrecision(3)
                    .HasColumnName("SHARECOUNT");

                entity.Property(e => e.SpouseName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SPOUSE_NAME");

                entity.Property(e => e.SpousePre)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SPOUSE_PRE");

                entity.Property(e => e.Street)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("STREET");

                entity.Property(e => e.WhatsappNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("WHATSAPP_NUMBER");
            });

            modelBuilder.Entity<EmpMaster>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("EMP_MASTER");

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
                    .HasColumnName("BOND_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.Category)
                    .HasPrecision(3)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.DaFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DA_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.DepartmentId)
                    .HasPrecision(4)
                    .HasColumnName("DEPARTMENT_ID");

                entity.Property(e => e.DesignationId)
                    .HasPrecision(3)
                    .HasColumnName("DESIGNATION_ID");

                entity.Property(e => e.EmpCode)
                    .HasPrecision(10)
                    .HasColumnName("EMP_CODE");

                entity.Property(e => e.EmpName)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("EMP_NAME");

                entity.Property(e => e.EmpType)
                    .HasPrecision(1)
                    .HasColumnName("EMP_TYPE");

                entity.Property(e => e.EsiFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESI_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.FirmId)
                    .HasPrecision(2)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.GradeId)
                    .HasPrecision(2)
                    .HasColumnName("GRADE_ID");

                entity.Property(e => e.GratiFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GRATI_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.JoinDt)
                    .HasColumnType("DATE")
                    .HasColumnName("JOIN_DT");

                entity.Property(e => e.MediclaimFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MEDICLAIM_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.PaidAmt)
                    .HasColumnType("NUMBER(7,2)")
                    .HasColumnName("PAID_AMT");

                entity.Property(e => e.PfFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PF_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.PostId)
                    .HasPrecision(4)
                    .HasColumnName("POST_ID");

                entity.Property(e => e.Rejoining)
                    .HasPrecision(1)
                    .HasColumnName("REJOINING");

                entity.Property(e => e.SecurityDep)
                    .HasColumnType("NUMBER(7,2)")
                    .HasColumnName("SECURITY_DEP");

                entity.Property(e => e.ShiftId)
                    .HasPrecision(3)
                    .HasColumnName("SHIFT_ID");

                entity.Property(e => e.StampPaper)
                    .HasPrecision(1)
                    .HasColumnName("STAMP_PAPER");

                entity.Property(e => e.StatusId)
                    .HasPrecision(2)
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.TaxFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TAX_FLAG")
                    .IsFixedLength();
            });

            modelBuilder.Entity<EmployFirm>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("EMPLOY_FIRM");

                entity.HasIndex(e => new { e.EmpCode, e.FirmId }, "EMP_FIRM_KEY")
                    .IsUnique();

                entity.HasIndex(e => e.EmpCode, "EMP_PRM")
                    .IsUnique();

                entity.HasIndex(e => new { e.FirmId, e.EmpCode }, "IDX$$_2D9F0006");

                entity.HasIndex(e => e.FirmId, "I_EMP_FIRM_4");

                entity.Property(e => e.BranchId)
                    .HasPrecision(5)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.EmpCode)
                    .HasPrecision(7)
                    .HasColumnName("EMP_CODE");

                entity.Property(e => e.FirmId)
                    .HasPrecision(7)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.FromDt)
                    .HasColumnType("DATE")
                    .HasColumnName("FROM_DT");
            });

            modelBuilder.Entity<EmployeeMaster>(entity =>
            {
                entity.HasKey(e => e.EmpCode)
                    .HasName("P_EMPLOYEE_MASTER");

                entity.ToTable("EMPLOYEE_MASTER");

                entity.HasIndex(e => new { e.StatusId, e.EmpCode }, "IDX$$_00010041");

                entity.HasIndex(e => new { e.EmpCode, e.StatusId }, "IDX$$_00010047");

                entity.HasIndex(e => new { e.EmpCode, e.FirmId, e.DepartmentId, e.DesignationId, e.PostId }, "I_EMPLOYEE_MASTER1");

                entity.HasIndex(e => e.BranchId, "I_EMPLOYEE_MASTER_10");

                entity.HasIndex(e => e.DesignationId, "I_EMPLOYEE_MASTER_5");

                entity.HasIndex(e => e.DepartmentId, "I_EMPLOYEE_MASTER_7");

                entity.HasIndex(e => e.StatusId, "I_EMPLOYEE_MASTER_9");

                entity.Property(e => e.EmpCode)
                    .HasPrecision(10)
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
                    .HasColumnName("BOND_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.Category)
                    .HasPrecision(3)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.DaFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("DA_FLAG")
                    .IsFixedLength();

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
                    .HasPrecision(1)
                    .HasColumnName("EMP_TYPE");

                entity.Property(e => e.EsiFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESI_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.FirmId)
                    .HasPrecision(2)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.GradeId)
                    .HasPrecision(2)
                    .HasColumnName("GRADE_ID");

                entity.Property(e => e.GratiFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GRATI_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.JoinDt)
                    .HasColumnType("DATE")
                    .HasColumnName("JOIN_DT");

                entity.Property(e => e.MediclaimFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MEDICLAIM_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.PaidAmt)
                    .HasColumnType("NUMBER(7,2)")
                    .HasColumnName("PAID_AMT");

                entity.Property(e => e.Password)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD")
                    .HasDefaultValueSql("null\n");

                entity.Property(e => e.PfFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("PF_FLAG")
                    .IsFixedLength();

                entity.Property(e => e.PostId)
                    .HasPrecision(4)
                    .HasColumnName("POST_ID");

                entity.Property(e => e.Rejoining)
                    .HasPrecision(1)
                    .HasColumnName("REJOINING");

                entity.Property(e => e.SecurityDep)
                    .HasColumnType("NUMBER(7,2)")
                    .HasColumnName("SECURITY_DEP");

                entity.Property(e => e.ShiftId)
                    .HasPrecision(3)
                    .HasColumnName("SHIFT_ID");

                entity.Property(e => e.StampPaper)
                    .HasPrecision(1)
                    .HasColumnName("STAMP_PAPER");

                entity.Property(e => e.StatusId)
                    .HasPrecision(2)
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.TaxFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TAX_FLAG")
                    .IsFixedLength();
            });

            modelBuilder.Entity<GeneralParameter>(entity =>
            {
                entity.HasKey(e => new { e.FirmId, e.ModuleId, e.ParmtrId })
                    .HasName("P_GEN_PARM");

                entity.ToTable("GENERAL_PARAMETER");

                entity.Property(e => e.FirmId)
                    .HasPrecision(3)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.ModuleId)
                    .HasPrecision(2)
                    .HasColumnName("MODULE_ID");

                entity.Property(e => e.ParmtrId)
                    .HasPrecision(3)
                    .HasColumnName("PARMTR_ID");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_TYPE")
                    .IsFixedLength();

                entity.Property(e => e.ParmtrName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARMTR_NAME");

                entity.Property(e => e.ParmtrValue)
                    .HasMaxLength(42)
                    .IsUnicode(false)
                    .HasColumnName("PARMTR_VALUE");

                entity.Property(e => e.SubLedger)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SUB_LEDGER")
                    .IsFixedLength();
            });

            modelBuilder.Entity<ModeOfTransfer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MODE_OF_TRANSFER");

                entity.Property(e => e.TransferMode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TRANSFER_MODE");
            });

            modelBuilder.Entity<ModeOfTranspost>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MODE_OF_TRANSPOST");

                entity.Property(e => e.Transpost)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TRANSPOST");
            });

            modelBuilder.Entity<NeftBankMst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NEFT_BANK_MST");

                entity.HasIndex(e => e.IfscCode, "U_NEFT_BANK_MST")
                    .IsUnique();

                entity.Property(e => e.Abbr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ABBR");

                entity.Property(e => e.Address)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.BankId)
                    .HasPrecision(5)
                    .HasColumnName("BANK_ID");

                entity.Property(e => e.Bankname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("BANKNAME");

                entity.Property(e => e.Branch)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("BRANCH");

                entity.Property(e => e.Centre)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("CENTRE");

                entity.Property(e => e.DistId)
                    .HasPrecision(5)
                    .HasColumnName("DIST_ID");

                entity.Property(e => e.District)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DISTRICT");

                entity.Property(e => e.EnterBy)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ENTER_BY");

                entity.Property(e => e.EnterDt)
                    .HasColumnType("DATE")
                    .HasColumnName("ENTER_DT");

                entity.Property(e => e.IfscCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IFSC_CODE");

                entity.Property(e => e.State)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("STATE");

                entity.Property(e => e.StateId)
                    .HasPrecision(2)
                    .HasColumnName("STATE_ID");
            });

            modelBuilder.Entity<NeftCustomer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("NEFT_CUSTOMER");

                entity.Property(e => e.AccType)
                    .HasPrecision(2)
                    .HasColumnName("ACC_TYPE");

                entity.Property(e => e.AttachmentType)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ATTACHMENT_TYPE");

                entity.Property(e => e.BankId)
                    .HasPrecision(5)
                    .HasColumnName("BANK_ID");

                entity.Property(e => e.BeneficiaryAccount)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARY_ACCOUNT");

                entity.Property(e => e.BeneficiaryBranch)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARY_BRANCH");

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.CustId)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("CUST_ID");

                entity.Property(e => e.CustName)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("CUST_NAME");

                entity.Property(e => e.CustRefId)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("CUST_REF_ID");

                entity.Property(e => e.FirmId)
                    .HasPrecision(3)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.IdProof)
                    .HasColumnType("BLOB")
                    .HasColumnName("ID_PROOF");

                entity.Property(e => e.IfscCode)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("IFSC_CODE");

                entity.Property(e => e.MobileNumber)
                    .HasPrecision(13)
                    .HasColumnName("MOBILE_NUMBER");

                entity.Property(e => e.ModifyDt)
                    .HasColumnType("DATE")
                    .HasColumnName("MODIFY_DT");

                entity.Property(e => e.Moduleid)
                    .HasPrecision(3)
                    .HasColumnName("MODULEID");

                entity.Property(e => e.ReasonPhone)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("REASON_PHONE");

                entity.Property(e => e.RejectReason)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("REJECT_REASON");

                entity.Property(e => e.Status)
                    .HasPrecision(1)
                    .HasColumnName("STATUS");

                entity.Property(e => e.TraDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRA_DT");

                entity.Property(e => e.UserId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.VerifiedBy)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("VERIFIED_BY");

                entity.Property(e => e.VerifiedDt)
                    .HasColumnType("DATE")
                    .HasColumnName("VERIFIED_DT");

                entity.Property(e => e.VerifyStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("VERIFY_STATUS")
                    .IsFixedLength();
            });

            modelBuilder.Entity<PledgeMs189>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PLEDGE_MS189");

                entity.Property(e => e.CustId)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("CUST_ID");

                entity.Property(e => e.Fdate)
                    .HasColumnType("DATE")
                    .HasColumnName("FDATE");

                entity.Property(e => e.LetterRef)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LETTER_REF");

                entity.Property(e => e.ReasonBlock)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("REASON_BLOCK");

                entity.Property(e => e.ReasonRelease)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("REASON_RELEASE");

                entity.Property(e => e.RelAuthority)
                    .HasPrecision(7)
                    .HasColumnName("REL_AUTHORITY");

                entity.Property(e => e.SanctionId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("SANCTION_ID");

                entity.Property(e => e.Tdate)
                    .HasColumnType("DATE")
                    .HasColumnName("TDATE");
            });

            modelBuilder.Entity<SubsidaryMaster>(entity =>
            {
                entity.HasKey(e => new { e.BranchId, e.FirmId, e.ParentAcc, e.AccountNo })
                    .HasName("P_SUBSIDARY_MASTER");

                entity.ToTable("SUBSIDARY_MASTER");

                entity.HasIndex(e => new { e.BranchId, e.FirmId, e.ParentAcc }, "I_SUBSIDARY_MASTER");

                entity.HasIndex(e => new { e.ParentAcc, e.BranchId, e.FirmId, e.StatusId }, "I_SUBSIDARY_MASTER1");

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.FirmId)
                    .HasPrecision(3)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.ParentAcc)
                    .HasPrecision(6)
                    .HasColumnName("PARENT_ACC");

                entity.Property(e => e.AccountNo)
                    .HasPrecision(8)
                    .HasColumnName("ACCOUNT_NO");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("ACCOUNT_NAME");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER(15,2)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.StatusId)
                    .HasPrecision(2)
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.SubId)
                    .HasPrecision(2)
                    .HasColumnName("SUB_ID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE")
                    .IsFixedLength();
            });

            modelBuilder.Entity<TakeoverConfirmationDtl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TAKEOVER_CONFIRMATION_DTL");

                entity.Property(e => e.BankName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BANK_NAME");

                entity.Property(e => e.Beneficiaryaccountno)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARYACCOUNTNO");

                entity.Property(e => e.Beneficiaryname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARYNAME");

                entity.Property(e => e.Branchid)
                    .HasPrecision(6)
                    .HasColumnName("BRANCHID");

                entity.Property(e => e.Cash)
                    .HasPrecision(16)
                    .HasColumnName("CASH")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Firmid)
                    .HasPrecision(1)
                    .HasColumnName("FIRMID");

                entity.Property(e => e.GoldAmount)
                    .HasPrecision(16)
                    .HasColumnName("GOLD_AMOUNT");

                entity.Property(e => e.GoldCash)
                    .HasPrecision(16)
                    .HasColumnName("GOLD_CASH");

                entity.Property(e => e.GoldTranfer)
                    .HasPrecision(16)
                    .HasColumnName("GOLD_TRANFER");

                entity.Property(e => e.Ifsccode)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("IFSCCODE")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ImpsTransferAmt)
                    .HasPrecision(16)
                    .HasColumnName("IMPS_TRANSFER_AMT");

                entity.Property(e => e.Loanno)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("LOANNO");

                entity.Property(e => e.PledgeNo)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("PLEDGE_NO");

                entity.Property(e => e.TakeoverBalance)
                    .HasPrecision(16)
                    .HasColumnName("TAKEOVER_BALANCE");

                entity.Property(e => e.TakeoverCustId)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("TAKEOVER_CUST_ID");

                entity.Property(e => e.TraDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRA_DT");

                entity.Property(e => e.Transfer)
                    .HasPrecision(16)
                    .HasColumnName("TRANSFER")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Userid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USERID");
            });

            modelBuilder.Entity<TakeoverloanDtl>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TAKEOVERLOAN_DTL");

                entity.HasIndex(e => e.LoanNo, "DFS")
                    .IsUnique();

                entity.Property(e => e.AmountOur)
                    .HasColumnType("NUMBER(12,2)")
                    .HasColumnName("AMOUNT_OUR");

                entity.Property(e => e.BankAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("BANK_ADDRESS");

                entity.Property(e => e.BankName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("BANK_NAME");

                entity.Property(e => e.BnkName)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("BNK_NAME");

                entity.Property(e => e.CarryingEmp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CARRYING_EMP");

                entity.Property(e => e.ChqNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CHQ_NO");

                entity.Property(e => e.CircularStat)
                    .HasPrecision(1)
                    .HasColumnName("CIRCULAR_STAT");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("CUSTOMER_ID");

                entity.Property(e => e.Distance)
                    .HasPrecision(5)
                    .HasColumnName("DISTANCE");

                entity.Property(e => e.KycStat)
                    .HasPrecision(1)
                    .HasColumnName("KYC_STAT");

                entity.Property(e => e.LoanNo)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("LOAN_NO");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("REASON");

                entity.Property(e => e.TotalAmt)
                    .HasColumnType("NUMBER(12,2)")
                    .HasColumnName("TOTAL_AMT");

                entity.Property(e => e.TotalWt)
                    .HasColumnType("NUMBER(7,2)")
                    .HasColumnName("TOTAL_WT");

                entity.Property(e => e.TraDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRA_DT");

                entity.Property(e => e.TransactionDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRANSACTION_DT");

                entity.Property(e => e.Transport)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TRANSPORT");

                entity.Property(e => e.TransportDriver)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TRANSPORT_DRIVER");

                entity.Property(e => e.TransportNo)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TRANSPORT_NO");

                entity.Property(e => e.TransportType)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TRANSPORT_TYPE");
            });

            modelBuilder.Entity<TakeoverloanMst>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TAKEOVERLOAN_MST");

                entity.HasIndex(e => e.LoanNo, "DFSDF")
                    .IsUnique();

                entity.Property(e => e.ApprovedDt)
                    .HasColumnType("DATE")
                    .HasColumnName("APPROVED_DT");

                entity.Property(e => e.ApprovedDt2)
                    .HasColumnType("DATE")
                    .HasColumnName("APPROVED_DT_2");

                entity.Property(e => e.ApprovedId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("APPROVED_ID");

                entity.Property(e => e.ApprovedId2)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("APPROVED_ID_2");

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BANK_NAME");

                entity.Property(e => e.BeneficiaryName)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARY_NAME");

                entity.Property(e => e.BenenficiaryAccount)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("BENENFICIARY_ACCOUNT");

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.ClsDt)
                    .HasColumnType("DATE")
                    .HasColumnName("CLS_DT");

                entity.Property(e => e.ConfirmDt)
                    .HasColumnType("DATE")
                    .HasColumnName("CONFIRM_DT");

                entity.Property(e => e.ConfirmId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CONFIRM_ID");

                entity.Property(e => e.CustId)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("CUST_ID");

                entity.Property(e => e.CustName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CUST_NAME");

                entity.Property(e => e.Document)
                    .HasColumnType("BLOB")
                    .HasColumnName("DOCUMENT");

                entity.Property(e => e.EffDt)
                    .HasColumnType("DATE")
                    .HasColumnName("EFF_DT");

                entity.Property(e => e.FirmId)
                    .HasPrecision(2)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.GoldDtlAmt)
                    .HasColumnType("NUMBER(12,2)")
                    .HasColumnName("GOLD_DTL_AMT")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.IfscCode)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("IFSC_CODE");

                entity.Property(e => e.LoanNo)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("LOAN_NO");

                entity.Property(e => e.ModuleId)
                    .HasPrecision(2)
                    .HasColumnName("MODULE_ID");

                entity.Property(e => e.PayMode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PAY_MODE");

                entity.Property(e => e.RecomendDt)
                    .HasColumnType("DATE")
                    .HasColumnName("RECOMEND_DT");

                entity.Property(e => e.RecomendId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("RECOMEND_ID");

                entity.Property(e => e.RequestDt)
                    .HasColumnType("DATE")
                    .HasColumnName("REQUEST_DT");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("REQUEST_ID");

                entity.Property(e => e.ReturnReason)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("RETURN_REASON");

                entity.Property(e => e.ReturnedAmt)
                    .HasColumnType("NUMBER(12,2)")
                    .HasColumnName("RETURNED_AMT");

                entity.Property(e => e.ReturnedBy)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("RETURNED_BY");

                entity.Property(e => e.ReturnedDt)
                    .HasColumnType("DATE")
                    .HasColumnName("RETURNED_DT");

                entity.Property(e => e.SettleId)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("SETTLE_ID");

                entity.Property(e => e.StatusId)
                    .HasPrecision(2)
                    .HasColumnName("STATUS_ID");

                entity.Property(e => e.TakeoverAmt)
                    .HasColumnType("NUMBER(12,2)")
                    .HasColumnName("TAKEOVER_AMT");

                entity.Property(e => e.TraDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRA_DT");
            });

            modelBuilder.Entity<Takeoverloanquotreq>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TAKEOVERLOANQUOTREQ");

                entity.Property(e => e.AmtOur)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMT_OUR");

                entity.Property(e => e.Bankaddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANKADDRESS");

                entity.Property(e => e.Bankdtl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANKDTL");

                entity.Property(e => e.Bankname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BANKNAME");

                entity.Property(e => e.Banknm)
                    .HasMaxLength(80)
                    .IsUnicode(false)
                    .HasColumnName("BANKNM");

                entity.Property(e => e.Beneficiaryaccountno)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARYACCOUNTNO");

                entity.Property(e => e.Beneficiaryname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("BENEFICIARYNAME");

                entity.Property(e => e.Cash)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CASH")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Circular)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CIRCULAR");

                entity.Property(e => e.Distance)
                    .HasColumnType("NUMBER")
                    .HasColumnName("DISTANCE");

                entity.Property(e => e.Ifsccode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("IFSCCODE")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Kyc)
                    .HasColumnType("NUMBER")
                    .HasColumnName("KYC");

                entity.Property(e => e.LoanAmt)
                    .HasColumnType("NUMBER")
                    .HasColumnName("LOAN_AMT");

                entity.Property(e => e.PbranchId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PBRANCH_ID");

                entity.Property(e => e.PcustId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PCUST_ID");

                entity.Property(e => e.PfirmId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PFIRM_ID");

                entity.Property(e => e.TakeAmt)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TAKE_AMT");

                entity.Property(e => e.Totalwt)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTALWT");

                entity.Property(e => e.TraDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRA_DT");

                entity.Property(e => e.Transfer)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TRANSFER")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Transport)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TRANSPORT");

                entity.Property(e => e.Userid)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USERID");

                entity.Property(e => e.Vehicledtl)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("VEHICLEDTL");
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TRANSACTION_DETAIL");

                entity.HasIndex(e => new { e.RefId, e.AccountNo }, "IDX$$_00010021");

                entity.HasIndex(e => new { e.AccountNo, e.Descr, e.BranchId }, "IDX$$_00010071");

                entity.HasIndex(e => new { e.FirmId, e.BranchId, e.TransId }, "I_BRID");

                entity.HasIndex(e => e.ModuleId, "TRANSACTION_DET_IDX$$_2D010000");

                entity.Property(e => e.AccountNo)
                    .HasPrecision(6)
                    .HasColumnName("ACCOUNT_NO");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER(15,2)")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.BranchId)
                    .HasPrecision(4)
                    .HasColumnName("BRANCH_ID");

                entity.Property(e => e.ContraNo)
                    .HasPrecision(6)
                    .HasColumnName("CONTRA_NO");

                entity.Property(e => e.Descr)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("DESCR");

                entity.Property(e => e.FirmId)
                    .HasPrecision(3)
                    .HasColumnName("FIRM_ID");

                entity.Property(e => e.ModuleId)
                    .HasPrecision(2)
                    .HasColumnName("MODULE_ID");

                entity.Property(e => e.Narration)
                    .HasMaxLength(175)
                    .IsUnicode(false)
                    .HasColumnName("NARRATION");

                entity.Property(e => e.PayMode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("PAY_MODE")
                    .IsFixedLength();

                entity.Property(e => e.RefId)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("REF_ID");

                entity.Property(e => e.SegmentId)
                    .HasPrecision(3)
                    .HasColumnName("SEGMENT_ID")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.TraDt)
                    .HasColumnType("DATE")
                    .HasColumnName("TRA_DT");

                entity.Property(e => e.TransId)
                    .HasPrecision(8)
                    .HasColumnName("TRANS_ID");

                entity.Property(e => e.Transno)
                    .HasPrecision(8)
                    .HasColumnName("TRANSNO");

                entity.Property(e => e.Type)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("TYPE")
                    .IsFixedLength();

                entity.Property(e => e.UserId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("USER_ID");

                entity.Property(e => e.ValueDt)
                    .HasColumnType("DATE")
                    .HasColumnName("VALUE_DT");
            });

            modelBuilder.Entity<VehicleName>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("VEHICLE_NAME");

                entity.Property(e => e.SelectVehicle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("SELECT_VEHICLE");
            });

            modelBuilder.HasSequence("ACC_SEQ").IsCyclic();

            modelBuilder.HasSequence("AUDIT_ID_SEQ");

            modelBuilder.HasSequence("CIRCULAR_SEQ").IsCyclic();

            modelBuilder.HasSequence("CLIENT_SEQ");

            modelBuilder.HasSequence("COLL_SEQ").IsCyclic();

            modelBuilder.HasSequence("CUST_CODE");

            modelBuilder.HasSequence("DEBALLOTNO_SEQ");

            modelBuilder.HasSequence("DOOSTEP_CUSTID");

            modelBuilder.HasSequence("DOOSTEP_REQID");

            modelBuilder.HasSequence("EMAIL_SEQ");

            modelBuilder.HasSequence("ENTITY_ID_SEQ");

            modelBuilder.HasSequence("J_LEAVE");

            modelBuilder.HasSequence("MACARE_ACC_ENTRY_TEMP_ID");

            modelBuilder.HasSequence("MACARE_ACC_TRAN_UNIQUE_ID");

            modelBuilder.HasSequence("MACARE_API_REQ_LOG_ID");

            modelBuilder.HasSequence("MACARE_API_USER_ID");

            modelBuilder.HasSequence("MAFOUND_ACC_ENTRY_TEMP_ID");

            modelBuilder.HasSequence("MAFOUND_ACC_TRAN_UNIQUE_ID");

            modelBuilder.HasSequence("MAFOUND_API_EXCEPTION_ID");

            modelBuilder.HasSequence("MAFOUND_API_REQ_LOG_ID");

            modelBuilder.HasSequence("MAFOUND_API_USER_ID");

            modelBuilder.HasSequence("MAIL_CALENDAR_SEQUENCE").IsCyclic();

            modelBuilder.HasSequence("MAIL_SEQ").IsCyclic();

            modelBuilder.HasSequence("MAIL_SEQ_TEMP").IsCyclic();

            modelBuilder.HasSequence("MAIL_SEQUENCE").IsCyclic();

            modelBuilder.HasSequence("MB_ACCESSOVERRIDE_ID");

            modelBuilder.HasSequence("MB_FORMACCESS_ID");

            modelBuilder.HasSequence("MB_PASSGENID");

            modelBuilder.HasSequence("MB_ROLEID_SEQ");

            modelBuilder.HasSequence("MB_SESSION_ID_SEQ");

            modelBuilder.HasSequence("MB_USERID");

            modelBuilder.HasSequence("MICROSOFTSEQDTPROPERTIES");

            modelBuilder.HasSequence("ONE_SEQ");

            modelBuilder.HasSequence("ORACLESEQCOUNT");

            modelBuilder.HasSequence("PLEDGE_SEQ").IsCyclic();

            modelBuilder.HasSequence("PLSQL_PROFILER_RUNNUMBER");

            modelBuilder.HasSequence("PRECLOSURESERIES");

            modelBuilder.HasSequence("QA_DEFFECT_IDS").IncrementsBy(-1020);

            modelBuilder.HasSequence("QA_MEDIAFILEID_SEQ").IsCyclic();

            modelBuilder.HasSequence("REGISTER_SEQ");

            modelBuilder.HasSequence("RENT_MABEN");

            modelBuilder.HasSequence("RENT_TDS_CERTIFICATE").IsCyclic();

            modelBuilder.HasSequence("REP_SEQ").IsCyclic();

            modelBuilder.HasSequence("RT_ID");

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

            modelBuilder.HasSequence("SEQ_DOCTORDTL");

            modelBuilder.HasSequence("SEQ_FEEDBACK_ID");

            modelBuilder.HasSequence("SEQ_FUND");

            modelBuilder.HasSequence("SEQ_INSU_COM_ID");

            modelBuilder.HasSequence("SEQ_INV");

            modelBuilder.HasSequence("SEQ_IP_ID");

            modelBuilder.HasSequence("SEQ_ITEM_ID");

            modelBuilder.HasSequence("SEQ_ITPROJECT_ERRORID");

            modelBuilder.HasSequence("SEQ_MB_FORMID");

            modelBuilder.HasSequence("SEQ_MB_SESSION_ID");

            modelBuilder.HasSequence("SEQ_NEW_FA");

            modelBuilder.HasSequence("SEQ_POLU_ID");

            modelBuilder.HasSequence("SEQ_PRE_CUSTID");

            modelBuilder.HasSequence("SEQ_PRE_CUSTID_KYC").IsCyclic();

            modelBuilder.HasSequence("SEQ_PRE_CUSTID1");

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

            modelBuilder.HasSequence("TASKMGMT_MEMBER_ALLOCATION_ID");

            modelBuilder.HasSequence("TASKMGMT_PROJECT_ID");

            modelBuilder.HasSequence("TASKMGMT_SESSION_ID");

            modelBuilder.HasSequence("TASKMGMT_TASK_ID");

            modelBuilder.HasSequence("TASKMGMT_USER_ID");

            modelBuilder.HasSequence("TDS_CERTIFICATE").IsCyclic();

            modelBuilder.HasSequence("TEAM_ID_SEQ");

            modelBuilder.HasSequence("TEMP_SEQ");

            modelBuilder.HasSequence("TESTSEQ");

            modelBuilder.HasSequence("TRUSTEESERIES");

            modelBuilder.HasSequence("TYPE_ID_SEQ");

            modelBuilder.HasSequence("VAPT_ASSIGNED_ID_SEQ").IsCyclic();

            modelBuilder.HasSequence("VAPT_ISSUE_ID_SEQ");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
