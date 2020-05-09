﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SpotifyApp.API.Data;

namespace SpotifyApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200503081916_CommentEntityAdded")]
    partial class CommentEntityAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("SpotifyApp.API.Models.Album", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.AlbumRate", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlbumId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rate")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "AlbumId");

                    b.HasIndex("AlbumId");

                    b.ToTable("AlbumRates");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.Artist", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.ArtistRate", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArtistId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Rate")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("RatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ArtistId");

                    b.HasIndex("ArtistId");

                    b.ToTable("ArtistRates");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AlbumId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentContent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CommentSent")
                        .HasColumnType("TEXT");

                    b.Property<int>("CommenterId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.HasIndex("CommenterId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("About")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SpotifyApp.API.Models.AlbumRate", b =>
                {
                    b.HasOne("SpotifyApp.API.Models.Album", "Album")
                        .WithMany("Rates")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SpotifyApp.API.Models.User", "User")
                        .WithMany("AlbumRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyApp.API.Models.ArtistRate", b =>
                {
                    b.HasOne("SpotifyApp.API.Models.Artist", "Artist")
                        .WithMany("Rates")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SpotifyApp.API.Models.User", "User")
                        .WithMany("ArtistRates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyApp.API.Models.Comment", b =>
                {
                    b.HasOne("SpotifyApp.API.Models.Album", "Album")
                        .WithMany("Comments")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SpotifyApp.API.Models.User", "Commenter")
                        .WithMany("Comments")
                        .HasForeignKey("CommenterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("SpotifyApp.API.Models.Photo", b =>
                {
                    b.HasOne("SpotifyApp.API.Models.User", "User")
                        .WithOne("Photo")
                        .HasForeignKey("SpotifyApp.API.Models.Photo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
