using board.Models;
using Microsoft.EntityFrameworkCore;

namespace board
{
    public partial class BoardDbContext:DbContext
    {
        
        
        public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options)
        {
            
        }
        
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AttachedFile> AttachedFiles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Recommendation> Recommendations { get; set; }
        public virtual DbSet<Sympathy> Sympathies { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedYn).HasColumnName("deleted_yn");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Answer_Member");
            });

            modelBuilder.Entity<AttachedFile>(entity =>
            {
                entity.HasKey(e => e.AttacehdfileId)
                    .HasName("PK__Attached__7B8970BB6D65712A");

                entity.ToTable("AttachedFile");

                entity.Property(e => e.AttacehdfileId).HasColumnName("attacehdfile_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedYn).HasColumnName("deleted_yn");

                entity.Property(e => e.FileSize).HasColumnName("file_size");

                entity.Property(e => e.FileType)
                    .HasMaxLength(20)
                    .HasColumnName("file_type");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.OriginFileName)
                    .HasMaxLength(100)
                    .HasColumnName("origin_file_name");

                entity.Property(e => e.OriginFilePath)
                    .HasMaxLength(100)
                    .HasColumnName("origin_file_path");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.PostType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("post_type")
                    .IsFixedLength(true);

                entity.Property(e => e.SaveFileName)
                    .HasMaxLength(100)
                    .HasColumnName("save_file_name");

                entity.Property(e => e.SaveFilePath)
                    .HasMaxLength(100)
                    .HasColumnName("save_file_path");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.AttachedFiles)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_File_Member");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("category_id");

                entity.Property(e => e.CategoryLevel).HasColumnName("category_level");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("category_name");

                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_Category");
            });
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedYn).HasColumnName("deleted_yn");

                entity.Property(e => e.Depth).HasColumnName("depth");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ParentCommentId).HasColumnName("parent_comment_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.PostType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("post_type")
                    .IsFixedLength(true);

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Comment_Member");

                entity.HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("FK_Comment");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Introduction)
                    .HasMaxLength(50)
                    .HasColumnName("introduction");

                entity.Property(e => e.MailAddress)
                    .HasMaxLength(30)
                    .HasColumnName("mail_address");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(10)
                    .HasColumnName("member_name");

                entity.Property(e => e.MemberPassword)
                    .HasMaxLength(12)
                    .HasColumnName("member_password");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.NotificationId).HasColumnName("notification_id");

                entity.Property(e => e.CheckedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("checked_at");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Notification_Member");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.DeletedYn).HasColumnName("deleted_yn");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(20)
                    .HasColumnName("title");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.ViewedCnt).HasColumnName("viewed_cnt");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Question_Member");
            });

            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.ToTable("Recommendation");

                entity.Property(e => e.RecommendationId).HasColumnName("recommendation_id");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");

                entity.Property(e => e.DeletedYn).HasColumnName("deleted_yn");

                entity.Property(e => e.IsPositive).HasColumnName("is_positive");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_Recommendation_Answer");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Recommendation_Member");
            });

            modelBuilder.Entity<Sympathy>(entity =>
            {
                entity.ToTable("Sympathy");

                entity.Property(e => e.SympathyId).HasColumnName("sympathy_id");

                entity.Property(e => e.DeletedYn).HasColumnName("deleted_yn");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Sympathies)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Sympathy_Member");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Sympathies)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Recommendation_Question");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}