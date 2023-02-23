﻿// <auto-generated />
using System;
using Lavyn.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lavyn.Persistence.Migrations
{
    [DbContext(typeof(DaoContext))]
    partial class DaoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lavyn.Domain.Entities.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date");

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint")
                        .HasColumnName("room_id");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint")
                        .HasColumnName("sender_id");

                    b.HasKey("Id")
                        .HasName("pk_messages");

                    b.HasIndex("RoomId");

                    b.HasIndex("SenderId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Key")
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("key");

                    b.Property<DateTime>("LastMessageDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_message_date");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_rooms");

                    b.HasIndex("Key");

                    b.HasIndex("Type");

                    b.ToTable("rooms", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Token", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<string>("Value")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("value");

                    b.HasKey("Id")
                        .HasName("pk_tokens");

                    b.HasIndex("Type");

                    b.HasIndex("UserId");

                    b.ToTable("tokens", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("boolean")
                        .HasColumnName("is_online");

                    b.Property<DateTime>("LastLogin")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_login");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("password");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("IsOnline");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.UserHasRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_has_roles");

                    b.HasIndex("RoleId");

                    b.ToTable("user_has_roles", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.UserHasRoom", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("LastSeen")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_seen");

                    b.Property<long>("RoomId")
                        .HasColumnType("bigint")
                        .HasColumnName("room_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_has_room");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("user_has_room", (string)null);
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Message", b =>
                {
                    b.HasOne("Lavyn.Domain.Entities.Room", "Room")
                        .WithMany("Messages")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_messages_rooms_room_id");

                    b.HasOne("Lavyn.Domain.Entities.User", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_messages_user_sender_id");

                    b.Navigation("Room");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Token", b =>
                {
                    b.HasOne("Lavyn.Domain.Entities.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_tokens_user_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.UserHasRole", b =>
                {
                    b.HasOne("Lavyn.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_has_roles_role_role_id");

                    b.HasOne("Lavyn.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_has_roles_user_user_id");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.UserHasRoom", b =>
                {
                    b.HasOne("Lavyn.Domain.Entities.Room", "Room")
                        .WithMany("UserHasRoom")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_has_room_rooms_room_id");

                    b.HasOne("Lavyn.Domain.Entities.User", "User")
                        .WithMany("UserRooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_has_room_user_user_id");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.Room", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("UserHasRoom");
                });

            modelBuilder.Entity("Lavyn.Domain.Entities.User", b =>
                {
                    b.Navigation("SentMessages");

                    b.Navigation("Tokens");

                    b.Navigation("UserRoles");

                    b.Navigation("UserRooms");
                });
#pragma warning restore 612, 618
        }
    }
}
