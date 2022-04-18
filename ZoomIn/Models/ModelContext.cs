using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ZoomIn.Models
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

        public virtual DbSet<Accessloger> Accesslogers { get; set; }
        public virtual DbSet<Balance> Balances { get; set; }
        public virtual DbSet<Balancetransaction> Balancetransactions { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
        public virtual DbSet<Creditcard> Creditcards { get; set; }
        public virtual DbSet<Enduser> Endusers { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Itemcategory> Itemcategories { get; set; }
        public virtual DbSet<Itemtype> Itemtypes { get; set; }
        public virtual DbSet<Loginloger> Loginlogers { get; set; }
        public virtual DbSet<Shopreview> Shopreviews { get; set; }
        public virtual DbSet<Userlikeitem> Userlikeitems { get; set; }
        public virtual DbSet<Userpurchaseitem> Userpurchaseitems { get; set; }
        public virtual DbSet<Userrole> Userroles { get; set; }
        public virtual DbSet<Usertransaction> Usertransactions { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=train_user153;PASSWORD=Ahmad_Tahaluf_2021$;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TRAIN_USER153")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<Accessloger>(entity =>
            {
                entity.HasKey(e => e.AccessId)
                    .HasName("ACCESS_PK");

                entity.ToTable("ACCESSLOGER");

                entity.Property(e => e.AccessId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ACCESS_ID");

                entity.Property(e => e.AccessCounter)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ACCESS_COUNTER")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.AccessDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ACCESS_DATE");

                entity.Property(e => e.AccessPage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCESS_PAGE");

                entity.Property(e => e.AccessVisitor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ACCESS_VISITOR");
            });

            modelBuilder.Entity<Balance>(entity =>
            {
                entity.HasKey(e => e.BalanceLock)
                    .HasName("BALANCE_PK");

                entity.ToTable("BALANCE");

                entity.Property(e => e.BalanceLock)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BALANCE_LOCK")
                    .HasDefaultValueSql("'X' ")
                    .IsFixedLength(true);

                entity.Property(e => e.BalanceValue)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BALANCEVALUE")
                    .HasDefaultValueSql("0.0");

                entity.Property(e => e.Profitrate)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PROFITRATE")
                    .HasDefaultValueSql("0.1");
            });

            modelBuilder.Entity<Balancetransaction>(entity =>
            {
                entity.HasKey(e => e.BtransactionId)
                    .HasName("BTRANSACTION_PK");

                entity.ToTable("BALANCETRANSACTION");

                entity.HasIndex(e => e.PurchaseId, "SYS_C0075298")
                    .IsUnique();

                entity.Property(e => e.BtransactionId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BTRANSACTION_ID");

                entity.Property(e => e.Balancelock)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("BALANCELOCK")
                    .HasDefaultValueSql("'X'")
                    .IsFixedLength(true);

                entity.Property(e => e.Btdescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("BTDESCRIPTION");

                entity.Property(e => e.Btransactiondate)
                    .HasColumnType("DATE")
                    .HasColumnName("BTRANSACTIONDATE");

                entity.Property(e => e.Btransactionvalue)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BTRANSACTIONVALUE");

                entity.Property(e => e.FromId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FROM_ID");

                entity.Property(e => e.Paymenttotal)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PAYMENTTOTAL");

                entity.Property(e => e.PurchaseId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PURCHASE_ID");

                entity.Property(e => e.Relatedsiterate)
                    .HasColumnType("FLOAT")
                    .HasColumnName("RELATEDSITERATE");

                entity.HasOne(d => d.BalancelockNavigation)
                    .WithMany(p => p.Balancetransactions)
                    .HasForeignKey(d => d.Balancelock)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("BTRANSACTION_FK3");

                entity.HasOne(d => d.From)
                    .WithMany(p => p.Balancetransactions)
                    .HasForeignKey(d => d.FromId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("BTRANSACTION_FK2");

                entity.HasOne(d => d.Purchase)
                    .WithOne(p => p.Balancetransaction)
                    .HasForeignKey<Balancetransaction>(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("BTRANSACTION_FK1");
            });

            modelBuilder.Entity<Configuration>(entity =>
            {
                entity.HasKey(e => e.ConfigLock)
                    .HasName("CONFIG_PK");

                entity.ToTable("CONFIGURATIONS");

                entity.Property(e => e.ConfigLock)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CONFIG_LOCK")
                    .HasDefaultValueSql("'X' ")
                    .IsFixedLength(true);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Facebookurl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FACEBOOKURL");

                entity.Property(e => e.Instagramurl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("INSTAGRAMURL");

                entity.Property(e => e.Linkedinurl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LINKEDINURL");

                entity.Property(e => e.Pinteresturl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PINTERESTURL");

                entity.Property(e => e.Websiteurl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WEBSITEURL");

                entity.Property(e => e.Address_main)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_MAIN");

                entity.Property(e => e.Address_sec)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS_SEC");

                entity.Property(e => e.Locationembed)
                            .HasMaxLength(300)
                            .IsUnicode(false)
                            .HasColumnName("LOCATIONEMBED");

                entity.Property(e => e.Locationurl)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("LOCATIONURL");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Twitterurl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("TWITTERURL");
            });

            modelBuilder.Entity<Creditcard>(entity =>
            {
                entity.HasKey(e => e.CardId)
                    .HasName("CK_PK");

                entity.ToTable("CREDITCARD");

                entity.HasIndex(e => e.Cardnumber, "SYS_C0075242")
                    .IsUnique();

                entity.Property(e => e.CardId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CARD_ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("FLOAT")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.Cardkey)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDKEY");

                entity.Property(e => e.Cardnumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARDNUMBER");

                entity.Property(e => e.Expirydate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRYDATE");
            });

            modelBuilder.Entity<Enduser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("ENDUSER_PK");

                entity.HasIndex(e => new { e.UserUsername, e.UserEmail }, "ENDUSER_UNIQUE")
                    .IsUnique();

                entity.ToTable("ENDUSER");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Birthday)
                    .HasColumnType("DATE")
                    .HasColumnName("BIRTHDAY");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CITY");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.CreditcId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CREDITC_ID");

                entity.Property(e => e.Fname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("GENDER")
                    .IsFixedLength(true);

                entity.Property(e => e.UserImage)
                    .HasColumnType("BLOB")
                    .IsRequired(true)
                    .HasColumnName("USERIMAGE");

                entity.Property(e => e.Lname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Registerdate)
                    .HasColumnType("DATE")
                    .HasColumnName("REGISTERDATE");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(true)
                    .HasColumnName("USER_EMAIL");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(true)
                    .HasColumnName("USER_PASSWORD");

                entity.Property(e => e.UserUsername)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(true)
                    .HasColumnName("USER_USERNAME");

                entity.Property(e => e.ImageExtension)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired(true)
                    .HasColumnName("IMAGEEXTENSION");

                entity.HasOne(d => d.Creditc)
                    .WithMany(p => p.Endusers)
                    .HasForeignKey(d => d.CreditcId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ENDUSER_FK1");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Endusers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ENDUSER_FK2");
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("ITEM");

                entity.Property(e => e.ItemId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEM_ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Createdate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATEDATE");

                entity.Property(e => e.Height)
                    .HasColumnType("NUMBER")
                    .HasColumnName("HEIGHT");

                entity.Property(e => e.ItemInnerFile)
                    .HasColumnType("BLOB")
                    .IsRequired(true)
                    .HasColumnName("ITEMFILE");

                entity.Property(e => e.ItemExtension)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsRequired(true)
                    .HasColumnName("ITEMEXTENSION");

                entity.Property(e => e.Itemdescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ITEMDESCRIPTION");

                entity.Property(e => e.Itemname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ITEMNAME");

                entity.Property(e => e.Noviews)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NOVIEWS")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Nolikes)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NOLIKES")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Nopurchases)
                    .HasColumnType("NUMBER")
                    .HasColumnName("NOPURCHASES")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Popularity)
                    .HasColumnType("NUMBER")
                    .HasColumnName("POPULARITY")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.OwnerId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("OWNER_ID");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Takenlocation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TAKENLOCATION");

                entity.Property(e => e.TypeId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("TYPE_ID");

                entity.Property(e => e.Uploaddate)
                    .HasColumnType("DATE")
                    .HasColumnName("UPLOADDATE");

                entity.Property(e => e.Width)
                    .HasColumnType("NUMBER")
                    .HasColumnName("WIDTH");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ITEM_FK2");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("ITEM_FK3");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("ITEM_FK1");
            });

            modelBuilder.Entity<Itemcategory>(entity =>
            {
                entity.HasKey(e => e.CateId)
                    .HasName("ITEMCATEGORY_PK");

                entity.ToTable("ITEMCATEGORY");

                entity.Property(e => e.CateId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CATE_ID");

                entity.Property(e => e.Catename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATENAME");

                entity.Property(e => e.Createdate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATEDATE");
            });

            modelBuilder.Entity<Itemtype>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("ITEMTYPE_PK");

                entity.ToTable("ITEMTYPE");

                entity.HasIndex(e => e.Typename, "SYS_C0075271")
                    .IsUnique();

                entity.Property(e => e.TypeId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TYPE_ID");

                entity.Property(e => e.Createdate)
                    .HasColumnType("DATE")
                    .HasColumnName("CREATEDATE");

                entity.Property(e => e.Typedescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TYPEDESCRIPTION");

                entity.Property(e => e.Typename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TYPENAME");
            });

            modelBuilder.Entity<Loginloger>(entity =>
            {
                entity.HasKey(e => e.LoginId)
                    .HasName("LOGINLOGER_PK");

                entity.ToTable("LOGINLOGER");

                entity.Property(e => e.LoginId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LOGIN_ID");

                entity.Property(e => e.LoginCounter)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("LOGIN_COUNTER")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.LoginDate)
                    .HasColumnType("DATE")
                    .HasColumnName("LOGIN_DATE");

                entity.Property(e => e.LoginUserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("LOGIN_USER_ID");

                entity.HasOne(d => d.LoginUser)
                    .WithMany(p => p.Loginlogers)
                    .HasForeignKey(d => d.LoginUserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LOGINLOGER_FK1");
            });

            modelBuilder.Entity<Shopreview>(entity =>
            {
                entity.HasKey(e => e.ReviewId)
                    .HasName("SHOPREVIEW_PK");

                entity.ToTable("SHOPREVIEW");

                entity.HasIndex(e => e.ReviewerId, "SYS_C0075265")
                    .IsUnique();

                entity.Property(e => e.ReviewId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("REVIEW_ID");

                entity.Property(e => e.Rate)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("RATE");

                entity.Property(e => e.Reviewcomment)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("REVIEWCOMMENT");

                entity.Property(e => e.ReviewerId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("REVIEWER_ID");

                entity.Property(e => e.ReviewDate)
                    .HasColumnType("DATE")
                    .HasColumnName("REVIEWDATE");

                entity.HasOne(d => d.Reviewer)
                    .WithOne(p => p.Shopreview)
                    .HasForeignKey<Shopreview>(d => d.ReviewerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SHOPREVIEW_FK1");
            });

            modelBuilder.Entity<Userlikeitem>(entity =>
            {
                entity.HasKey(e => e.LikeId)
                    .HasName("LIKE_PK");

                entity.ToTable("USERLIKEITEM");

                entity.Property(e => e.LikeId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LIKE_ID");

                entity.Property(e => e.ItemId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ITEM_ID");

                entity.Property(e => e.Likedate)
                    .HasColumnType("DATE")
                    .HasColumnName("LIKEDATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Userlikeitems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LIKE_FK2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userlikeitems)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("LIKE_FK1");
            });

            modelBuilder.Entity<Userpurchaseitem>(entity =>
            {
                entity.HasKey(e => e.PurchaseId)
                    .HasName("PURCHASE_PK");

                entity.ToTable("USERPURCHASEITEM");

                entity.Property(e => e.PurchaseId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PURCHASE_ID");

                entity.Property(e => e.BuyerId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.ItemId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ITEM_ID");

                entity.Property(e => e.Paymenttotal)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PAYMENTTOTAL");

                entity.Property(e => e.Purchasedate)
                    .HasColumnType("DATE")
                    .HasColumnName("PURCHASEDATE");

                entity.Property(e => e.Relatedsiterate)
                    .HasColumnType("FLOAT")
                    .HasColumnName("RELATEDSITERATE");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Userpurchaseitems)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PURCHASE_FK1");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Userpurchaseitems)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("PURCHASE_FK2");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("USERROLE_PK");

                entity.ToTable("USERROLE");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLE_ID");

                entity.Property(e => e.Roledescription)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ROLEDESCRIPTION");

                entity.Property(e => e.Roleindex)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLEINDEX");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<Usertransaction>(entity =>
            {
                entity.HasKey(e => e.UtransactionId)
                    .HasName("UTRANSACTION_PK");

                entity.ToTable("USERTRANSACTION");

                entity.HasIndex(e => e.PurchaseId, "SYS_C0075292")
                    .IsUnique();

                entity.Property(e => e.UtransactionId)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("UTRANSACTION_ID");

                entity.Property(e => e.FromId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("FROM_ID");

                entity.Property(e => e.Paymenttotal)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PAYMENTTOTAL");

                entity.Property(e => e.PurchaseId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PURCHASE_ID");

                entity.Property(e => e.Relatedsiterate)
                    .HasColumnType("FLOAT")
                    .HasColumnName("RELATEDSITERATE");

                entity.Property(e => e.ToId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("TO_ID");

                entity.Property(e => e.Utdescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("UTDESCRIPTION");

                entity.Property(e => e.Utransactiondate)
                    .HasColumnType("DATE")
                    .HasColumnName("UTRANSACTIONDATE");

                entity.Property(e => e.Utransactionvalue)
                    .HasColumnType("FLOAT")
                    .HasColumnName("UTRANSACTIONVALUE");

                entity.HasOne(d => d.From)
                    .WithMany(p => p.UsertransactionFroms)
                    .HasForeignKey(d => d.FromId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("UTRANSACTION_FK2");

                entity.HasOne(d => d.Purchase)
                    .WithOne(p => p.Usertransaction)
                    .HasForeignKey<Usertransaction>(d => d.PurchaseId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("UTRANSACTION_FK1");

                entity.HasOne(d => d.To)
                    .WithMany(p => p.UsertransactionTos)
                    .HasForeignKey(d => d.ToId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("UTRANSACTION_FK3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
