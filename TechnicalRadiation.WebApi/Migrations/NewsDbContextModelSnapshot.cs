﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechnicalRadiation.Repositories.Data;

namespace TechnicalRadiation.WebApi.Migrations
{
    [DbContext(typeof(NewsDbContext))]
    partial class NewsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("AuthorNewsItem", b =>
                {
                    b.Property<int>("AuthorsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewsItemsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorsId", "NewsItemsId");

                    b.HasIndex("NewsItemsId");

                    b.ToTable("AuthorNewsItem");
                });

            modelBuilder.Entity("CategoryNewsItem", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NewsItemsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoriesId", "NewsItemsId");

                    b.HasIndex("NewsItemsId");

                    b.ToTable("CategoryNewsItem");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bio")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileImgSource")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Slug")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TechnicalRadiation.Models.Entities.NewsItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgSource")
                        .HasColumnType("TEXT");

                    b.Property<string>("LongDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("NewsItems");
                });

            modelBuilder.Entity("AuthorNewsItem", b =>
                {
                    b.HasOne("TechnicalRadiation.Models.Entities.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechnicalRadiation.Models.Entities.NewsItem", null)
                        .WithMany()
                        .HasForeignKey("NewsItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryNewsItem", b =>
                {
                    b.HasOne("TechnicalRadiation.Models.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TechnicalRadiation.Models.Entities.NewsItem", null)
                        .WithMany()
                        .HasForeignKey("NewsItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
