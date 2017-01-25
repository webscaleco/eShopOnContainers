﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.eShopOnContainers.Services.Ordering.Infrastructure;

namespace Ordering.API.Migrations
{
    [DbContext(typeof(OrderingContext))]
    [Migration("20170125143653_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:Sequence:.orderitemseq", "'orderitemseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:Sequence:ordering.buyerseq", "'buyerseq', 'ordering', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:Sequence:ordering.orderseq", "'orderseq', 'ordering', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:Sequence:ordering.paymentseq", "'paymentseq', 'ordering', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "buyerseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "ordering")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("FullName")
                        .IsUnique();

                    b.ToTable("buyers","ordering");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.CardType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("cardtypes","ordering");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "paymentseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "ordering")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Alias")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("BuyerId");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<int>("CardTypeId");

                    b.Property<DateTime>("Expiration");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("CardTypeId");

                    b.ToTable("payments","ordering");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "orderseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "ordering")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("BuyerId");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<DateTime>("OrderDate");

                    b.Property<int>("OrderStatusId");

                    b.Property<int>("PaymentId");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("ZipCode")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("PaymentId");

                    b.ToTable("orders","ordering");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "orderitemseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<decimal>("Discount");

                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<decimal>("UnitPrice");

                    b.Property<int>("Units");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems","ordering");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("orderstatus","ordering");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.Payment", b =>
                {
                    b.HasOne("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer")
                        .WithMany("Payments")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.CardType", "CardType")
                        .WithMany()
                        .HasForeignKey("CardTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.Buyer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");
                });

            modelBuilder.Entity("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Microsoft.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate.Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}