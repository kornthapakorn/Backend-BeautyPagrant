using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend_BeautyPagrant.Models;

public partial class BeautyPagrantContext : DbContext
{
    public BeautyPagrantContext()
    {
    }

    public BeautyPagrantContext(DbContextOptions<BeautyPagrantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AboutU> AboutUs { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<BirthDate> BirthDates { get; set; }

    public virtual DbSet<BirthDateResult> BirthDateResults { get; set; }

    public virtual DbSet<Button> Buttons { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactPicture> ContactPictures { get; set; }

    public virtual DbSet<Date> Dates { get; set; }

    public virtual DbSet<DateResult> DateResults { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCategorize> EventCategorizes { get; set; }

    public virtual DbSet<EventComponent> EventComponents { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

    public virtual DbSet<FormButton> FormButtons { get; set; }

    public virtual DbSet<FormComponent> FormComponents { get; set; }

    public virtual DbSet<FormComponentTemplate> FormComponentTemplates { get; set; }

    public virtual DbSet<FormTemplate> FormTemplates { get; set; }

    public virtual DbSet<GridFourImage> GridFourImages { get; set; }

    public virtual DbSet<GridTwoColumn> GridTwoColumns { get; set; }

    public virtual DbSet<ImageDesc> ImageDescs { get; set; }

    public virtual DbSet<ImageUpload> ImageUploads { get; set; }

    public virtual DbSet<ImageUploadResult> ImageUploadResults { get; set; }

    public virtual DbSet<ImageUploadWithImageContent> ImageUploadWithImageContents { get; set; }

    public virtual DbSet<ImageUploadWithImageContentResult> ImageUploadWithImageContentResults { get; set; }

    public virtual DbSet<ImageWithCaption> ImageWithCaptions { get; set; }

    public virtual DbSet<OneTopicImageCaptionButton> OneTopicImageCaptionButtons { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<SingleSelection> SingleSelections { get; set; }

    public virtual DbSet<SingleSelectionResult> SingleSelectionResults { get; set; }

    public virtual DbSet<TableWithTopicAndDesc> TableWithTopicAndDescs { get; set; }

    public virtual DbSet<TextBox> TextBoxes { get; set; }

    public virtual DbSet<TextField> TextFields { get; set; }

    public virtual DbSet<TextFieldResult> TextFieldResults { get; set; }

    public virtual DbSet<TwoTopicImageCaptionButton> TwoTopicImageCaptionButtons { get; set; }

    public virtual DbSet<userr> userrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MMYWAIFU_LABTOP\\SQLEXPRESS;Initial Catalog= BeautyPagrant;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactPicture>(entity =>
        {
            entity.HasOne(d => d.Contact).WithMany(p => p.ContactPictures).HasConstraintName("FK_ContactPicture_Contact");
        });

        modelBuilder.Entity<EventCategorize>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EventCatorize");

            entity.HasOne(d => d.Category).WithMany(p => p.EventCategorizes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventCatorize_Category");

            entity.HasOne(d => d.Event).WithMany(p => p.EventCategorizes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventCatorize_Event");
        });

        modelBuilder.Entity<EventComponent>(entity =>
        {
            entity.HasOne(d => d.AboutU).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_AboutU");

            entity.HasOne(d => d.Banner).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_Banner");

            entity.HasOne(d => d.Button).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_Button");

            entity.HasOne(d => d.Event).WithMany(p => p.EventComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EventComponent_Event");

            entity.HasOne(d => d.FormTemplate).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_FormTemplate");

            entity.HasOne(d => d.GridFourImage).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_GridFourImage");

            entity.HasOne(d => d.GridTwoColumn).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_GridTwoColumn");

            entity.HasOne(d => d.ImageDesc).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_ImageDesc");

            entity.HasOne(d => d.ImageWithCaption).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_ImageWithCaption");

            entity.HasOne(d => d.OneTopicImageCationButton).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_OneTopicImageCaptionButton");

            entity.HasOne(d => d.Sale).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_Sale");

            entity.HasOne(d => d.Section).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_Section");

            entity.HasOne(d => d.TableWithTopicAndDesc).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_TableWithTopicAndDesc");

            entity.HasOne(d => d.Textbox).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_TextBox");

            entity.HasOne(d => d.TwoTopicImageCationButton).WithMany(p => p.EventComponents).HasConstraintName("FK_EventComponent_TwoTopicImageCaptionButton");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Form_1");

            entity.HasOne(d => d.FormTemplate).WithMany(p => p.Forms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Form_FormTemplate");
        });

        modelBuilder.Entity<FormComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FormComponent_1");

            entity.HasOne(d => d.BirthDateResult).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_BirthDateResult");

            entity.HasOne(d => d.DateResult).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_DateResult");

            entity.HasOne(d => d.FormComponentTemplate).WithMany(p => p.FormComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormComponent_FormComponentTemplate");

            entity.HasOne(d => d.Form).WithMany(p => p.FormComponents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormComponent_Form1");

            entity.HasOne(d => d.ImageUploadResult).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_ImageUploadResult");

            entity.HasOne(d => d.ImageUploadWithImageContentResult).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_ImageUploadWithImageContentResult");

            entity.HasOne(d => d.SingleSelectionResult).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_SingleSelectionResult");

            entity.HasOne(d => d.TextFieldResult).WithMany(p => p.FormComponents).HasConstraintName("FK_FormComponent_TextFieldResult");
        });

        modelBuilder.Entity<FormComponentTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FormComponent");

            entity.HasOne(d => d.BirthDate).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_BirthDate");

            entity.HasOne(d => d.Date).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_Date");

            entity.HasOne(d => d.FormButton).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_FormButton");

            entity.HasOne(d => d.Form).WithMany(p => p.FormComponentTemplates)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormComponent_Form");

            entity.HasOne(d => d.ImageUpload).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_ImageUpload");

            entity.HasOne(d => d.ImageUploadWithImageContent).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_ImageUploadWithImageContent");

            entity.HasOne(d => d.SingleSelection).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_SingleSelection");

            entity.HasOne(d => d.TextFieldNavigation).WithMany(p => p.FormComponentTemplates).HasConstraintName("FK_FormComponentTemplate_TextField");
        });

        modelBuilder.Entity<FormTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Form");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_userr");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
