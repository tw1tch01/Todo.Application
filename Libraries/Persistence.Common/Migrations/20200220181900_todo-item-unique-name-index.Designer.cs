﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Todo.Persistence.Common;

namespace Todo.Persistence.Common.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20200220181900_todo-item-unique-name-index")]
    partial class todoitemuniquenameindex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Todo.Domain.Entities.TodoItem", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("CancelledOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("CompletedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedProcess")
                        .IsRequired()
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImportanceLevel")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedProcess")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4")
                        .HasMaxLength(64);

                    b.Property<Guid?>("ParentItemId")
                        .HasColumnType("char(36)");

                    b.Property<string>("PriorityLevel")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ItemId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Todo.Domain.Entities.TodoItemNote", b =>
                {
                    b.Property<Guid>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedProcess")
                        .IsRequired()
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<Guid>("ItemId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ModifiedProcess")
                        .HasColumnType("varchar(1024) CHARACTER SET utf8mb4")
                        .HasMaxLength(1024);

                    b.Property<Guid?>("ParentNoteId")
                        .HasColumnType("char(36)");

                    b.HasKey("NoteId");

                    b.HasIndex("ItemId");

                    b.HasIndex("ParentNoteId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Todo.Domain.Entities.TodoItem", b =>
                {
                    b.HasOne("Todo.Domain.Entities.TodoItem", "ParentItem")
                        .WithMany("ChildItems")
                        .HasForeignKey("ParentItemId");
                });

            modelBuilder.Entity("Todo.Domain.Entities.TodoItemNote", b =>
                {
                    b.HasOne("Todo.Domain.Entities.TodoItem", "Item")
                        .WithMany("Notes")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Todo.Domain.Entities.TodoItemNote", "ParentNote")
                        .WithMany("Replies")
                        .HasForeignKey("ParentNoteId");
                });
#pragma warning restore 612, 618
        }
    }
}
