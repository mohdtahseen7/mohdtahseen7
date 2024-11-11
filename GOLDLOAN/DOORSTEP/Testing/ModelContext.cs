using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DOORSTEP.Testing;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblDoorstepConfiguration> TblDoorstepConfigurations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("User Id=gl_uat;Password=pass#1234;Data Source=10.192.5.76:1521/macuatpdb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("GL_UAT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<TblDoorstepConfiguration>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TBL_DOORSTEP_CONFIGURATION");

            entity.Property(e => e.MaximumGram)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MAXIMUM_GRAM");
            entity.Property(e => e.MaximumScheduletime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAXIMUM_SCHEDULETIME");
            entity.Property(e => e.MinimumGram)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MINIMUM_GRAM");
            entity.Property(e => e.MinimumWithdrawamt)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("MINIMUM_WITHDRAWAMT");
            entity.Property(e => e.OtpblockTime)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("OTPBLOCK_TIME");
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
