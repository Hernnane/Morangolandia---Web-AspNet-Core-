﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MorangoWeb3.Data;

#nullable disable

namespace MorangoWeb3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241110135911_MudandoTabelaUsuariosModel-ConfigurandoApelidoNaoNulo")]
    partial class MudandoTabelaUsuariosModelConfigurandoApelidoNaoNulo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MorangoWeb3.Models.CurtidasModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataCurtida")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdReceita")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("receitasModelId")
                        .HasColumnType("int");

                    b.Property<int>("usuariosModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("receitasModelId");

                    b.HasIndex("usuariosModelId");

                    b.ToTable("Curtidas");
                });

            modelBuilder.Entity("MorangoWeb3.Models.ReceitasModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataPostagem")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagemCaminho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ingredientes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int>("usuariosModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("usuariosModelId");

                    b.ToTable("Receitas");
                });

            modelBuilder.Entity("MorangoWeb3.Models.SalvamentosModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataSalvamento")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdReceita")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("receitasModelId")
                        .HasColumnType("int");

                    b.Property<int>("usuariosModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("receitasModelId");

                    b.HasIndex("usuariosModelId");

                    b.ToTable("Salvamentos");
                });

            modelBuilder.Entity("MorangoWeb3.Models.UsuariosModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apelido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FotoPerfilCaminho")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Idade")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MorangoWeb3.Models.CurtidasModel", b =>
                {
                    b.HasOne("MorangoWeb3.Models.ReceitasModel", "receitasModel")
                        .WithMany()
                        .HasForeignKey("receitasModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MorangoWeb3.Models.UsuariosModel", "usuariosModel")
                        .WithMany()
                        .HasForeignKey("usuariosModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("receitasModel");

                    b.Navigation("usuariosModel");
                });

            modelBuilder.Entity("MorangoWeb3.Models.ReceitasModel", b =>
                {
                    b.HasOne("MorangoWeb3.Models.UsuariosModel", "usuariosModel")
                        .WithMany()
                        .HasForeignKey("usuariosModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("usuariosModel");
                });

            modelBuilder.Entity("MorangoWeb3.Models.SalvamentosModel", b =>
                {
                    b.HasOne("MorangoWeb3.Models.ReceitasModel", "receitasModel")
                        .WithMany()
                        .HasForeignKey("receitasModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MorangoWeb3.Models.UsuariosModel", "usuariosModel")
                        .WithMany()
                        .HasForeignKey("usuariosModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("receitasModel");

                    b.Navigation("usuariosModel");
                });
#pragma warning restore 612, 618
        }
    }
}
