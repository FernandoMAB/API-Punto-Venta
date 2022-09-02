using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Punto_Venta.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAJA",
                columns: table => new
                {
                    CAJ_ID = table.Column<int>(type: "int", nullable: false),
                    CAJ_MON_1_C = table.Column<int>(type: "int", nullable: true),
                    CAJ_MON_5_C = table.Column<int>(type: "int", nullable: true),
                    CAJ_MON_10_C = table.Column<int>(type: "int", nullable: true),
                    CAJ_MON_25_C = table.Column<int>(type: "int", nullable: true),
                    CAJ_MON_50_C = table.Column<int>(type: "int", nullable: true),
                    CAJ_MON_1_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_1_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_2_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_5_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_10_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_20_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_50_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_BILL_100_DOL = table.Column<int>(type: "int", nullable: true),
                    CAJ_REG_INGRESO = table.Column<double>(type: "float", nullable: true),
                    CAJ_REG_SALIDA = table.Column<double>(type: "float", nullable: true),
                    CAJ_TOTAL = table.Column<double>(type: "float", nullable: true),
                    CAJ_FECHA = table.Column<DateTime>(type: "datetime", nullable: true),
                    CAJ_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAJA", x => x.CAJ_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIA",
                columns: table => new
                {
                    CAT_ID = table.Column<int>(type: "int", nullable: false),
                    CAT_DESCRIP = table.Column<string>(type: "text", nullable: true),
                    CAT_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA", x => x.CAT_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "CLIENTE",
                columns: table => new
                {
                    CLI_ID = table.Column<int>(type: "int", nullable: false),
                    CLI_P_NOMBRE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CLI_P_APELLIDO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CLI_S_NOMBRE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CLI_S_APELLIDO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CLI_TIPO_IDEN = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CLI_NUMERO_IDEN = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CLI_DIRECCION = table.Column<string>(type: "text", nullable: true),
                    CLI_EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CLI_NUM_CELULAR = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CLI_TELEFONO = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    CLI_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLIENTE", x => x.CLI_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "COMPRA",
                columns: table => new
                {
                    COM_ID = table.Column<int>(type: "int", nullable: false),
                    COM_FEC_INGRESO = table.Column<DateTime>(type: "datetime", nullable: true),
                    COM_IVA = table.Column<double>(type: "float", nullable: true),
                    COM_SUBTOTAL = table.Column<double>(type: "float", nullable: true),
                    COM_TOTAL = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPRA", x => x.COM_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "PARAMETROSG",
                columns: table => new
                {
                    PAR_ID = table.Column<int>(type: "int", nullable: false),
                    PAR_DESCRIP = table.Column<string>(type: "text", nullable: true),
                    PAR_VALOR = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    PAR_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARAMETROSG", x => x.PAR_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "ROL",
                columns: table => new
                {
                    ROL_ID = table.Column<int>(type: "int", nullable: false),
                    ROL_DESCRIP = table.Column<string>(type: "text", nullable: true),
                    ROL_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROL", x => x.ROL_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    USU_ID = table.Column<int>(type: "int", nullable: false),
                    USU_P_NOMBRE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USU_P_APELLIDO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USU_S_NOMBRE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USU_S_APELLIDO = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USU_CONTRASENA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USU_TIPO_IDEN = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    USU_NUMERO_IDEN = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    USU_FEC_NACIMIENTO = table.Column<DateTime>(type: "datetime", nullable: true),
                    USU_EST_CIVIL = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    USU_EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USU_TELEFONO = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    USU_NUM_CELULAR = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    USU_DIRECCION = table.Column<string>(type: "text", nullable: true),
                    USU_NUM_CARGAS = table.Column<int>(type: "int", nullable: true),
                    USU_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.USU_ID)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTO",
                columns: table => new
                {
                    PRO_ID = table.Column<int>(type: "int", nullable: false),
                    COM_ID = table.Column<int>(type: "int", nullable: true),
                    PRO_NOMBRE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PRO_DESCUENTO = table.Column<float>(type: "real", nullable: true),
                    PRO_PRECIO = table.Column<double>(type: "float", nullable: true),
                    PRO_COD_BARRAS = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    PRO_CATEGORIA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PRO_MARCA = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PRO_EST_IVA = table.Column<int>(type: "int", nullable: true),
                    PRO_DETALLE = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    PRO_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTO", x => x.PRO_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PRODUCTO_FK_PRODUC_COMPRA",
                        column: x => x.COM_ID,
                        principalTable: "COMPRA",
                        principalColumn: "COM_ID");
                });

            migrationBuilder.CreateTable(
                name: "PERMISOS",
                columns: table => new
                {
                    PER_ID = table.Column<int>(type: "int", nullable: false),
                    ROL_ID = table.Column<int>(type: "int", nullable: true),
                    PER_PANTALLA = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    PER_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISOS", x => x.PER_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_PERMISOS_FK_ROL_PE_ROL",
                        column: x => x.ROL_ID,
                        principalTable: "ROL",
                        principalColumn: "ROL_ID");
                });

            migrationBuilder.CreateTable(
                name: "FACTURA",
                columns: table => new
                {
                    FAC_ID = table.Column<int>(type: "int", nullable: false),
                    CLI_ID = table.Column<int>(type: "int", nullable: true),
                    USU_ID = table.Column<int>(type: "int", nullable: false),
                    FAC_FECHA = table.Column<DateTime>(type: "datetime", nullable: true),
                    FAC_SUBTOTAL = table.Column<double>(type: "float", nullable: true),
                    FAC_IVA = table.Column<double>(type: "float", nullable: true),
                    FAC_DESCUEN = table.Column<double>(type: "float", nullable: true),
                    FAC_TOTAL = table.Column<double>(type: "float", nullable: true),
                    FAC_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACTURA", x => x.FAC_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_FACTURA_FK_CLIENT_CLIENTE",
                        column: x => x.CLI_ID,
                        principalTable: "CLIENTE",
                        principalColumn: "CLI_ID");
                    table.ForeignKey(
                        name: "FK_FACTURA_FK_USUARI_USUARIO",
                        column: x => x.USU_ID,
                        principalTable: "USUARIO",
                        principalColumn: "USU_ID");
                });

            migrationBuilder.CreateTable(
                name: "FK_ROL_USUARIO",
                columns: table => new
                {
                    ROL_ID = table.Column<int>(type: "int", nullable: false),
                    USU_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FK_ROL_USUARIO", x => new { x.ROL_ID, x.USU_ID });
                    table.ForeignKey(
                        name: "FK_FK_ROL_U_FK_ROL_US_ROL",
                        column: x => x.ROL_ID,
                        principalTable: "ROL",
                        principalColumn: "ROL_ID");
                    table.ForeignKey(
                        name: "FK_FK_ROL_U_FK_ROL_US_USUARIO",
                        column: x => x.USU_ID,
                        principalTable: "USUARIO",
                        principalColumn: "USU_ID");
                });

            migrationBuilder.CreateTable(
                name: "FK_USUARIO_CAJA",
                columns: table => new
                {
                    USU_ID = table.Column<int>(type: "int", nullable: false),
                    CAJ_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FK_USUARIO_CAJA", x => new { x.USU_ID, x.CAJ_ID });
                    table.ForeignKey(
                        name: "FK_FK_USUAR_FK_USUARI_CAJA",
                        column: x => x.CAJ_ID,
                        principalTable: "CAJA",
                        principalColumn: "CAJ_ID");
                    table.ForeignKey(
                        name: "FK_FK_USUAR_FK_USUARI_USUARIO",
                        column: x => x.USU_ID,
                        principalTable: "USUARIO",
                        principalColumn: "USU_ID");
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIA_PRODUCTO",
                columns: table => new
                {
                    CA_PRO_ID = table.Column<int>(type: "int", nullable: false),
                    PRO_ID = table.Column<int>(type: "int", nullable: true),
                    CAT_ID = table.Column<int>(type: "int", nullable: true),
                    CA_PRO_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIA_PRODUCTO", x => x.CA_PRO_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_CATEGORI_FK_CATEGO_CATEGORI",
                        column: x => x.CAT_ID,
                        principalTable: "CATEGORIA",
                        principalColumn: "CAT_ID");
                    table.ForeignKey(
                        name: "FK_CATEGORI_FK_CATEGO_PRODUCTO",
                        column: x => x.PRO_ID,
                        principalTable: "PRODUCTO",
                        principalColumn: "PRO_ID");
                });

            migrationBuilder.CreateTable(
                name: "FACTURA_DETALLE",
                columns: table => new
                {
                    FAD_ID = table.Column<int>(type: "int", nullable: false),
                    FAC_ID = table.Column<int>(type: "int", nullable: false),
                    FAD_FECHA = table.Column<DateTime>(type: "datetime", nullable: true),
                    FAD_PRECIO_UNIT = table.Column<double>(type: "float", nullable: true),
                    FAD_CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    FAD_SUBTOTAL = table.Column<double>(type: "float", nullable: true),
                    FAD_IVA = table.Column<double>(type: "float", nullable: true),
                    FAD_DESCUENTO = table.Column<double>(type: "float", nullable: true),
                    FAD_TOTAL = table.Column<double>(type: "float", nullable: true),
                    FAD_NUMERACION = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FAD_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FACTURA_DETALLE", x => x.FAD_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_FACTURA__FK_DETALL_FACTURA",
                        column: x => x.FAC_ID,
                        principalTable: "FACTURA",
                        principalColumn: "FAC_ID");
                });

            migrationBuilder.CreateTable(
                name: "KARDEX",
                columns: table => new
                {
                    KAR_ID = table.Column<int>(type: "int", nullable: false),
                    FAC_ID = table.Column<int>(type: "int", nullable: true),
                    PRO_ID = table.Column<int>(type: "int", nullable: true),
                    COM_ID = table.Column<int>(type: "int", nullable: true),
                    KAR_FECHA = table.Column<DateTime>(type: "datetime", nullable: true),
                    KAR_DETALLE = table.Column<string>(type: "text", nullable: true),
                    KAR_ENT_CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    KAR_SAL_CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    KAR_ENT_PRE_TOTAL = table.Column<double>(type: "float", nullable: true),
                    KAR_SAL_PRE_TOTAL = table.Column<double>(type: "float", nullable: true),
                    KAR_BAL_CANTIDAD = table.Column<int>(type: "int", nullable: true),
                    KAR_BAL_PRECIO = table.Column<double>(type: "float", nullable: true),
                    KAR_BAL_PRECIO_TOTAL = table.Column<double>(type: "float", nullable: true),
                    KAR_ESTADO = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KARDEX", x => x.KAR_ID)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_KARDEX_FK_COMPRA_COMPRA",
                        column: x => x.COM_ID,
                        principalTable: "COMPRA",
                        principalColumn: "COM_ID");
                    table.ForeignKey(
                        name: "FK_KARDEX_FK_FACTUR_FACTURA",
                        column: x => x.FAC_ID,
                        principalTable: "FACTURA",
                        principalColumn: "FAC_ID");
                    table.ForeignKey(
                        name: "FK_KARDEX_FK_PRODUC_PRODUCTO",
                        column: x => x.PRO_ID,
                        principalTable: "PRODUCTO",
                        principalColumn: "PRO_ID");
                });

            migrationBuilder.CreateTable(
                name: "FK_PRODUCTO_DETALLEFACTURA",
                columns: table => new
                {
                    PRO_ID = table.Column<int>(type: "int", nullable: false),
                    FAD_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FK_PRODUCTO_DETALLEFACTURA", x => new { x.PRO_ID, x.FAD_ID });
                    table.ForeignKey(
                        name: "FK_FK_PRODU_FK_PRODUC_FACTURA_",
                        column: x => x.FAD_ID,
                        principalTable: "FACTURA_DETALLE",
                        principalColumn: "FAD_ID");
                    table.ForeignKey(
                        name: "FK_FK_PRODU_FK_PRODUC_PRODUCTO",
                        column: x => x.PRO_ID,
                        principalTable: "PRODUCTO",
                        principalColumn: "PRO_ID");
                });

            migrationBuilder.CreateIndex(
                name: "FK_CATEGORIA_CA_PRO_FK",
                table: "CATEGORIA_PRODUCTO",
                column: "CAT_ID");

            migrationBuilder.CreateIndex(
                name: "FK_CATEGORIA_PRODUCTO_FK",
                table: "CATEGORIA_PRODUCTO",
                column: "PRO_ID");

            migrationBuilder.CreateIndex(
                name: "FK_CLIENTE_FACTURA_FK",
                table: "FACTURA",
                column: "CLI_ID");

            migrationBuilder.CreateIndex(
                name: "FK_USUARIO_FACTURA_FK",
                table: "FACTURA",
                column: "USU_ID");

            migrationBuilder.CreateIndex(
                name: "FK_DETALLEFACTURA_FACTURA_FK",
                table: "FACTURA_DETALLE",
                column: "FAC_ID");

            migrationBuilder.CreateIndex(
                name: "FK_PRODUCTO_DETALLEFACTURA_FK",
                table: "FK_PRODUCTO_DETALLEFACTURA",
                column: "PRO_ID");

            migrationBuilder.CreateIndex(
                name: "FK_PRODUCTO_DETALLEFACTURA2_FK",
                table: "FK_PRODUCTO_DETALLEFACTURA",
                column: "FAD_ID");

            migrationBuilder.CreateIndex(
                name: "FK_ROL_USUARIO_FK",
                table: "FK_ROL_USUARIO",
                column: "ROL_ID");

            migrationBuilder.CreateIndex(
                name: "FK_ROL_USUARIO2_FK",
                table: "FK_ROL_USUARIO",
                column: "USU_ID");

            migrationBuilder.CreateIndex(
                name: "FK_USUARIO_CAJA_FK",
                table: "FK_USUARIO_CAJA",
                column: "USU_ID");

            migrationBuilder.CreateIndex(
                name: "FK_USUARIO_CAJA2_FK",
                table: "FK_USUARIO_CAJA",
                column: "CAJ_ID");

            migrationBuilder.CreateIndex(
                name: "FK_COMPRA_KARDEX_FK",
                table: "KARDEX",
                column: "COM_ID");

            migrationBuilder.CreateIndex(
                name: "FK_FACTURA_KARDEX_FK",
                table: "KARDEX",
                column: "FAC_ID");

            migrationBuilder.CreateIndex(
                name: "FK_PRODUCTO_KARDEX_FK",
                table: "KARDEX",
                column: "PRO_ID");

            migrationBuilder.CreateIndex(
                name: "FK_ROL_PERMISOS_FK",
                table: "PERMISOS",
                column: "ROL_ID");

            migrationBuilder.CreateIndex(
                name: "FK_PRODUCTO_COMPRA_FK",
                table: "PRODUCTO",
                column: "COM_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CATEGORIA_PRODUCTO");

            migrationBuilder.DropTable(
                name: "FK_PRODUCTO_DETALLEFACTURA");

            migrationBuilder.DropTable(
                name: "FK_ROL_USUARIO");

            migrationBuilder.DropTable(
                name: "FK_USUARIO_CAJA");

            migrationBuilder.DropTable(
                name: "KARDEX");

            migrationBuilder.DropTable(
                name: "PARAMETROSG");

            migrationBuilder.DropTable(
                name: "PERMISOS");

            migrationBuilder.DropTable(
                name: "CATEGORIA");

            migrationBuilder.DropTable(
                name: "FACTURA_DETALLE");

            migrationBuilder.DropTable(
                name: "CAJA");

            migrationBuilder.DropTable(
                name: "PRODUCTO");

            migrationBuilder.DropTable(
                name: "ROL");

            migrationBuilder.DropTable(
                name: "FACTURA");

            migrationBuilder.DropTable(
                name: "COMPRA");

            migrationBuilder.DropTable(
                name: "CLIENTE");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
