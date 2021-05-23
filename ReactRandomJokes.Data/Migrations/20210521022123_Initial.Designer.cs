﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactRandomJokes.Data;

namespace ReactRandomJokes.Data.Migrations
{
    [DbContext(typeof(JokesDbContext))]
    [Migration("20210521022123_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReactRandomJokes.Data.Joke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Punchline")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Setup")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jokes");
                });

            modelBuilder.Entity("ReactRandomJokes.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReactRandomJokes.Data.UserLikedJokes", b =>
                {
                    b.Property<int>("JokeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Liked")
                        .HasColumnType("bit");

                    b.HasKey("JokeId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLikedJokes");
                });

            modelBuilder.Entity("ReactRandomJokes.Data.UserLikedJokes", b =>
                {
                    b.HasOne("ReactRandomJokes.Data.Joke", "Joke")
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("JokeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ReactRandomJokes.Data.User", "User")
                        .WithMany("UserLikedJokes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Joke");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ReactRandomJokes.Data.Joke", b =>
                {
                    b.Navigation("UserLikedJokes");
                });

            modelBuilder.Entity("ReactRandomJokes.Data.User", b =>
                {
                    b.Navigation("UserLikedJokes");
                });
#pragma warning restore 612, 618
        }
    }
}