using board.Models;
using Microsoft.EntityFrameworkCore;

namespace board
{
    public partial class BoardDbContext:DbContext
    {
        
        
        public BoardDbContext(DbContextOptions<BoardDbContext> options) : base(options)
        {
            
        }
        
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Question> Questions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


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

                entity.Property(e => e.QuestionId).HasColumnName("question_id");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_Comment_Member");


                entity.HasOne(d => d.Question)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.QuestionId)
                    .HasConstraintName("FK_Comment_Question");

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
            
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}