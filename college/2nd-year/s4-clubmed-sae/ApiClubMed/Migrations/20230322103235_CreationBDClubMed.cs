using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiClubMed.Migrations
{
    /// <inheritdoc />
    public partial class CreationBDClubMed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_e_activiteenfantcarte_aec",
                columns: table => new
                {
                    aec_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aec_titre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    aec_duree = table.Column<int>(type: "integer", nullable: false),
                    aec_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    aec_frequence = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    aec_prix = table.Column<decimal>(type: "numeric(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activiteenfantcarte", x => x.aec_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_activiteenfantincluse_aei",
                columns: table => new
                {
                    aei_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aei_titre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    aei_duree = table.Column<int>(type: "integer", nullable: false),
                    aei_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    aei_frequence = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activiteenfantincluse", x => x.aei_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_categorietypechambre_ctc",
                columns: table => new
                {
                    ctc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ctc_libelle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categorietypechambre", x => x.ctc_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_civilite_cvl",
                columns: table => new
                {
                    cvl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cvl_libelle = table.Column<string>(type: "char(255)", maxLength: 255, nullable: false, defaultValue: "Monsieur")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_civilite", x => x.cvl_id);
                    table.CheckConstraint("ck_cvl_libelle", "cvl_libelle in ('Monsieur', 'Madame', 'Mademoiselle', 'Anonyme')");
                });

            migrationBuilder.CreateTable(
                name: "t_e_commodite_com",
                columns: table => new
                {
                    com_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    com_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_commodite", x => x.com_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_localisation_loc",
                columns: table => new
                {
                    loc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    loc_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localisation", x => x.loc_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pays_pay",
                columns: table => new
                {
                    pay_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_e_pays_pay", x => x.pay_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_photo_pht",
                columns: table => new
                {
                    pht_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pht_lien = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo", x => x.pht_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_pointfort_ptf",
                columns: table => new
                {
                    ptf_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ptf_libelle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pointfort", x => x.ptf_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_regroupementclub_rcl",
                columns: table => new
                {
                    rcl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rcl_libelle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regroupementclub", x => x.rcl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_souslocalisation_slo",
                columns: table => new
                {
                    slo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    slo_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_souslocalisation", x => x.slo_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_trancheage_tag",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_agemin = table.Column<int>(type: "integer", nullable: false),
                    tag_agemax = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trancheage", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_transport_tra",
                columns: table => new
                {
                    tra_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tra_libelle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_transport", x => x.tra_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeclub_tcl",
                columns: table => new
                {
                    tcl_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tcl_libelle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typeclub", x => x.tcl_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_video_vid",
                columns: table => new
                {
                    vid_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    vid_lien = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_video", x => x.vid_id);
                });

            migrationBuilder.CreateTable(
                name: "t_e_autreparticipant_apt",
                columns: table => new
                {
                    apt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cvl_civiliteid = table.Column<int>(type: "integer", nullable: false),
                    apt_prenom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    apt_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    apt_datenaissance = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "CURRENT_DATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_autreparticipant", x => x.apt_id);
                    table.CheckConstraint("ck_apt_datenaissance", "apt_datenaissance < current_date");
                    table.ForeignKey(
                        name: "fk_apt_cvl",
                        column: x => x.cvl_civiliteid,
                        principalTable: "t_e_civilite_cvl",
                        principalColumn: "cvl_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_client_cli",
                columns: table => new
                {
                    cli_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pay_paysid = table.Column<int>(type: "integer", nullable: false),
                    cvl_civiliteid = table.Column<int>(type: "integer", nullable: false),
                    cli_prenom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cli_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cli_datenaissance = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "CURRENT_DATE"),
                    cli_email = table.Column<string>(type: "text", nullable: false),
                    cli_tel = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cli_numrue = table.Column<int>(type: "integer", nullable: true),
                    cli_nomrue = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cli_codepostal = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cli_ville = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cli_password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cli_derniereconnexion = table.Column<DateTime>(type: "date", nullable: true),
                    cli_tempsrestant = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true, defaultValue: "0 ans 0 mois 0 jours")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client", x => x.cli_id);
                    table.CheckConstraint("ck_cli_datenaissance", "cli_datenaissance < current_date");
                    table.CheckConstraint("ck_cli_email", "cli_email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}$'");
                    table.ForeignKey(
                        name: "fk_cli_cvl",
                        column: x => x.cvl_civiliteid,
                        principalTable: "t_e_civilite_cvl",
                        principalColumn: "cvl_id");
                    table.ForeignKey(
                        name: "fk_cli_pys",
                        column: x => x.pay_paysid,
                        principalTable: "t_e_pays_pay",
                        principalColumn: "pay_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_domaineskiable_dms",
                columns: table => new
                {
                    dms_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pht_id = table.Column<int>(type: "integer", nullable: false),
                    dms_titre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dms_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dms_altitudeclub = table.Column<int>(type: "integer", nullable: false),
                    dms_altitudestation = table.Column<int>(type: "integer", nullable: false),
                    dms_nbpiste = table.Column<int>(type: "integer", nullable: false),
                    dms_infoskiaupied = table.Column<bool>(type: "boolean", nullable: false),
                    dms_description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    dms_longueurdespistes = table.Column<decimal>(type: "numeric(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_domaineskiable", x => x.dms_id);
                    table.ForeignKey(
                        name: "fk_pht_dms",
                        column: x => x.pht_id,
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_typeactivite_tac",
                columns: table => new
                {
                    tac_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pht_id = table.Column<int>(type: "integer", nullable: false),
                    tac_titre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tac_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    tac_nbactiviteincluse = table.Column<int>(type: "integer", nullable: false),
                    tac_nbactivitecarte = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typeactivite", x => x.tac_id);
                    table.ForeignKey(
                        name: "fk_pht_tac",
                        column: x => x.pht_id,
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_localisationsouslocalisation_lsl",
                columns: table => new
                {
                    loc_id = table.Column<int>(type: "integer", nullable: false),
                    slo_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_localisationsouslocalisation_lsl", x => new { x.loc_id, x.slo_id });
                    table.ForeignKey(
                        name: "FK_t_j_localisationsouslocalisation_lsl_t_e_localisation_loc_l~",
                        column: x => x.loc_id,
                        principalTable: "t_e_localisation_loc",
                        principalColumn: "loc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_localisationsouslocalisation_lsl_t_e_souslocalisation_s~",
                        column: x => x.slo_id,
                        principalTable: "t_e_souslocalisation_slo",
                        principalColumn: "slo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_activiteenfantcartetrancheage_act",
                columns: table => new
                {
                    aec_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activiteenfantcartetrancheage_act", x => new { x.aec_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_t_j_activiteenfantcartetrancheage_act_t_e_activiteenfantcar~",
                        column: x => x.aec_id,
                        principalTable: "t_e_activiteenfantcarte_aec",
                        principalColumn: "aec_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activiteenfantcartetrancheage_act_t_e_trancheage_tag_ta~",
                        column: x => x.tag_id,
                        principalTable: "t_e_trancheage_tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_activiteenfantinclusetrancheage_ait",
                columns: table => new
                {
                    aei_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activiteenfantinclusetrancheage_ait", x => new { x.aei_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_t_j_activiteenfantinclusetrancheage_ait_t_e_activiteenfanti~",
                        column: x => x.aei_id,
                        principalTable: "t_e_activiteenfantincluse_aei",
                        principalColumn: "aei_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activiteenfantinclusetrancheage_ait_t_e_trancheage_tag_~",
                        column: x => x.tag_id,
                        principalTable: "t_e_trancheage_tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_cartebanquaire_cba",
                columns: table => new
                {
                    cba_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cli_clientid = table.Column<int>(type: "integer", nullable: false),
                    cba_numerocarte = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    cba_dateexpiration = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    cba_cryptogramme = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cartebanquaire", x => x.cba_id);
                    table.CheckConstraint("ck_cba_dateexpiration", "cba_dateexpiration > CURRENT_DATE");
                    table.ForeignKey(
                        name: "fk_cba_cli",
                        column: x => x.cli_clientid,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_resort_res",
                columns: table => new
                {
                    res_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dms_domaineid = table.Column<int>(type: "integer", nullable: true),
                    loc_localisationid = table.Column<int>(type: "integer", nullable: false),
                    slo_souslocalisationid = table.Column<int>(type: "integer", nullable: false),
                    res_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    res_moyenneavis = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    res_descriptiongen = table.Column<string>(type: "text", nullable: false),
                    res_lienpdfdocclub = table.Column<string>(type: "text", nullable: false),
                    res_prixdepart = table.Column<decimal>(type: "numeric(8,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_resort", x => x.res_id);
                    table.CheckConstraint("ck_res_moyenneavis", "res_moyenneavis >= 0 AND res_moyenneavis <= 5");
                    table.CheckConstraint("ck_res_prixdepart", "res_prixdepart > 0");
                    table.ForeignKey(
                        name: "fk_res_dom",
                        column: x => x.dms_domaineid,
                        principalTable: "t_e_domaineskiable_dms",
                        principalColumn: "dms_id");
                    table.ForeignKey(
                        name: "fk_res_loc",
                        column: x => x.loc_localisationid,
                        principalTable: "t_e_localisation_loc",
                        principalColumn: "loc_id");
                    table.ForeignKey(
                        name: "fk_res_slo",
                        column: x => x.slo_souslocalisationid,
                        principalTable: "t_e_souslocalisation_slo",
                        principalColumn: "slo_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_activitecarte_aca",
                columns: table => new
                {
                    aca_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tac_id = table.Column<int>(type: "integer", nullable: false),
                    aca_titre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    aca_duree = table.Column<int>(type: "integer", nullable: false),
                    aca_description = table.Column<string>(type: "text", nullable: false),
                    aca_frequence = table.Column<string>(type: "text", nullable: false),
                    aca_agemin = table.Column<int>(type: "integer", nullable: false),
                    aca_prix = table.Column<decimal>(type: "numeric(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activitecarte", x => x.aca_id);
                    table.ForeignKey(
                        name: "FK_t_e_activitecarte_aca_t_e_typeactivite_tac_tac_id",
                        column: x => x.tac_id,
                        principalTable: "t_e_typeactivite_tac",
                        principalColumn: "tac_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_activiteincluse_aci",
                columns: table => new
                {
                    aci_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tac_id = table.Column<int>(type: "integer", nullable: false),
                    aci_titre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    aci_duree = table.Column<int>(type: "integer", nullable: true),
                    aci_description = table.Column<string>(type: "text", nullable: true),
                    aci_frequence = table.Column<string>(type: "text", nullable: false),
                    aci_agemin = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activiteincluse", x => x.aci_id);
                    table.ForeignKey(
                        name: "FK_t_e_activiteincluse_aci_t_e_typeactivite_tac_tac_id",
                        column: x => x.tac_id,
                        principalTable: "t_e_typeactivite_tac",
                        principalColumn: "tac_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_trancheagetypeactivite_tta",
                columns: table => new
                {
                    tac_id = table.Column<int>(type: "integer", nullable: false),
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_trancheagetypeactivite_tta", x => new { x.tac_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_t_j_trancheagetypeactivite_tta_t_e_trancheage_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "t_e_trancheage_tag",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_trancheagetypeactivite_tta_t_e_typeactivite_tac_tac_id",
                        column: x => x.tac_id,
                        principalTable: "t_e_typeactivite_tac",
                        principalColumn: "tac_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_avis_avi",
                columns: table => new
                {
                    avi_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    res_resortid = table.Column<int>(type: "integer", nullable: false),
                    cli_clientid = table.Column<int>(type: "integer", nullable: false),
                    pht_photoid = table.Column<int>(type: "integer", nullable: true),
                    avi_note = table.Column<int>(type: "integer", nullable: false),
                    avi_commentaire = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    avi_date = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_avis", x => x.avi_id);
                    table.CheckConstraint("ck_avi_note", "avi_note between 0 and 5");
                    table.ForeignKey(
                        name: "fk_avi_cli",
                        column: x => x.cli_clientid,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_id");
                    table.ForeignKey(
                        name: "fk_avi_res",
                        column: x => x.res_resortid,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id");
                    table.ForeignKey(
                        name: "fk_pht_avi",
                        column: x => x.pht_photoid,
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_bar_bar",
                columns: table => new
                {
                    bar_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pht_photoid = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    bar_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    bar_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bar", x => x.bar_id);
                    table.ForeignKey(
                        name: "fk_bar_res",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id");
                    table.ForeignKey(
                        name: "fk_pht_bar",
                        column: x => x.pht_photoid,
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_reservation_rsv",
                columns: table => new
                {
                    rsv_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cli_clientid = table.Column<int>(type: "integer", nullable: false),
                    tra_transportid = table.Column<int>(type: "integer", nullable: false),
                    res_resortid = table.Column<int>(type: "integer", nullable: false),
                    rsv_datedebut = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    rsv_datefin = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE + 1"),
                    rsv_prix = table.Column<decimal>(type: "numeric", nullable: false, defaultValue: 0m),
                    rsv_confirmation = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    rsv_validation = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservation", x => x.rsv_id);
                    table.CheckConstraint("ck_rsv_datefin", "rsv_datefin > rsv_datedebut");
                    table.CheckConstraint("ck_rsv_prix", "rsv_prix > 0");
                    table.ForeignKey(
                        name: "fk_rsv_cli",
                        column: x => x.cli_clientid,
                        principalTable: "t_e_client_cli",
                        principalColumn: "cli_id");
                    table.ForeignKey(
                        name: "fk_rsv_res",
                        column: x => x.res_resortid,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id");
                    table.ForeignKey(
                        name: "fk_rsv_tra",
                        column: x => x.tra_transportid,
                        principalTable: "t_e_transport_tra",
                        principalColumn: "tra_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_restaurant_rst",
                columns: table => new
                {
                    rst_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pht_photoid = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    bar_nom = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    bar_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_restaurant", x => x.rst_id);
                    table.ForeignKey(
                        name: "fk_pht_rst",
                        column: x => x.pht_photoid,
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_id");
                    table.ForeignKey(
                        name: "fk_rst_res",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id");
                });

            migrationBuilder.CreateTable(
                name: "t_e_typechambre_tpc",
                columns: table => new
                {
                    tpc_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    ctc_id = table.Column<int>(type: "integer", nullable: false),
                    tpc_surface = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    tpc_capacite = table.Column<int>(type: "integer", nullable: true),
                    tpc_presentation = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    tpc_libellecatgorie = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tpc_prixjournalier = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_typechambre", x => x.tpc_id);
                    table.CheckConstraint("ck_tpc_capacite", "tpc_capacite > 0 ");
                    table.CheckConstraint("ck_tpc_prixjournalier", "tpc_prixjournalier > 0 ");
                    table.CheckConstraint("ck_tpc_surface", "tpc_surface > 0 ");
                    table.ForeignKey(
                        name: "fk_tpc_ctc",
                        column: x => x.ctc_id,
                        principalTable: "t_e_categorietypechambre_ctc",
                        principalColumn: "ctc_id");
                    table.ForeignKey(
                        name: "fk_tpc_res",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_activitecarteenfantincluseresort_cir",
                columns: table => new
                {
                    aei_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activitecarteenfantincluseresort_cir", x => new { x.aei_id, x.res_id });
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteenfantincluseresort_cir_t_e_activiteenfant~",
                        column: x => x.aei_id,
                        principalTable: "t_e_activiteenfantincluse_aei",
                        principalColumn: "aei_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteenfantincluseresort_cir_t_e_resort_res_res~",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_activitecarteenfantresort_cer",
                columns: table => new
                {
                    aec_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activitecarteenfantresort_cer", x => new { x.aec_id, x.res_id });
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteenfantresort_cer_t_e_activiteenfantcarte_a~",
                        column: x => x.aec_id,
                        principalTable: "t_e_activiteenfantcarte_aec",
                        principalColumn: "aec_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteenfantresort_cer_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_regroupementclubresort_rcr",
                columns: table => new
                {
                    rcl_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_regroupementclubresort_rcr", x => new { x.rcl_id, x.res_id });
                    table.ForeignKey(
                        name: "FK_t_j_regroupementclubresort_rcr_t_e_regroupementclub_rcl_rcl~",
                        column: x => x.rcl_id,
                        principalTable: "t_e_regroupementclub_rcl",
                        principalColumn: "rcl_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_regroupementclubresort_rcr_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_resortphoto_rph",
                columns: table => new
                {
                    pht_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_resortphoto_rph", x => new { x.pht_id, x.res_id });
                    table.ForeignKey(
                        name: "FK_t_j_resortphoto_rph_t_e_photo_pht_pht_id",
                        column: x => x.pht_id,
                        principalTable: "t_e_photo_pht",
                        principalColumn: "pht_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_resortphoto_rph_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_resortvideo_rvi",
                columns: table => new
                {
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    vid_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_resortvideo_rvi", x => new { x.res_id, x.vid_id });
                    table.ForeignKey(
                        name: "FK_t_j_resortvideo_rvi_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_resortvideo_rvi_t_e_video_vid_vid_id",
                        column: x => x.vid_id,
                        principalTable: "t_e_video_vid",
                        principalColumn: "vid_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_typeclubresort_tcr",
                columns: table => new
                {
                    res_id = table.Column<int>(type: "integer", nullable: false),
                    tcl_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_typeclubresort_tcr", x => new { x.res_id, x.tcl_id });
                    table.ForeignKey(
                        name: "FK_t_j_typeclubresort_tcr_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_typeclubresort_tcr_t_e_typeclub_tcl_tcl_id",
                        column: x => x.tcl_id,
                        principalTable: "t_e_typeclub_tcl",
                        principalColumn: "tcl_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_activitecarteresort_acr",
                columns: table => new
                {
                    aca_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activitecarteresort_acr", x => new { x.aca_id, x.res_id });
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteresort_acr_t_e_activitecarte_aca_aca_id",
                        column: x => x.aca_id,
                        principalTable: "t_e_activitecarte_aca",
                        principalColumn: "aca_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteresort_acr_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_activiteincluseresort_air",
                columns: table => new
                {
                    aci_id = table.Column<int>(type: "integer", nullable: false),
                    res_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activiteincluseresort_air", x => new { x.aci_id, x.res_id });
                    table.ForeignKey(
                        name: "FK_t_j_activiteincluseresort_air_t_e_activiteincluse_aci_aci_id",
                        column: x => x.aci_id,
                        principalTable: "t_e_activiteincluse_aci",
                        principalColumn: "aci_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activiteincluseresort_air_t_e_resort_res_res_id",
                        column: x => x.res_id,
                        principalTable: "t_e_resort_res",
                        principalColumn: "res_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_e_payement_pyt",
                columns: table => new
                {
                    pyt_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    rsv_reservationid = table.Column<int>(type: "integer", nullable: false),
                    pyt_montant = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payement", x => x.pyt_id);
                    table.CheckConstraint("ck_pyt_montant", "pyt_montant > 0");
                    table.ForeignKey(
                        name: "fk_pyt_rsv",
                        column: x => x.rsv_reservationid,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id");
                });

            migrationBuilder.CreateTable(
                name: "t_j_activitecarteenfantreservation_cer",
                columns: table => new
                {
                    aec_id = table.Column<int>(type: "integer", nullable: false),
                    rsv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activitecarteenfantreservation_cer", x => new { x.aec_id, x.rsv_id });
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteenfantreservation_cer_t_e_activiteenfantca~",
                        column: x => x.aec_id,
                        principalTable: "t_e_activiteenfantcarte_aec",
                        principalColumn: "aec_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activitecarteenfantreservation_cer_t_e_reservation_rsv_~",
                        column: x => x.rsv_id,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_activitecartereservation_acr",
                columns: table => new
                {
                    aca_id = table.Column<int>(type: "integer", nullable: false),
                    rsv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_activitecartereservation_acr", x => new { x.aca_id, x.rsv_id });
                    table.ForeignKey(
                        name: "FK_t_j_activitecartereservation_acr_t_e_activitecarte_aca_aca_~",
                        column: x => x.aca_id,
                        principalTable: "t_e_activitecarte_aca",
                        principalColumn: "aca_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_activitecartereservation_acr_t_e_reservation_rsv_rsv_id",
                        column: x => x.rsv_id,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_reservationautreparticipant_sap",
                columns: table => new
                {
                    apt_id = table.Column<int>(type: "integer", nullable: false),
                    rsv_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_reservationautreparticipant_sap", x => new { x.apt_id, x.rsv_id });
                    table.ForeignKey(
                        name: "FK_t_j_reservationautreparticipant_sap_t_e_autreparticipant_ap~",
                        column: x => x.apt_id,
                        principalTable: "t_e_autreparticipant_apt",
                        principalColumn: "apt_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_reservationautreparticipant_sap_t_e_reservation_rsv_rsv~",
                        column: x => x.rsv_id,
                        principalTable: "t_e_reservation_rsv",
                        principalColumn: "rsv_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_typechambrecommodite_tpc",
                columns: table => new
                {
                    com_id = table.Column<int>(type: "integer", nullable: false),
                    tpc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_typechambrecommodite_tpc", x => new { x.com_id, x.tpc_id });
                    table.ForeignKey(
                        name: "FK_t_j_typechambrecommodite_tpc_t_e_commodite_com_com_id",
                        column: x => x.com_id,
                        principalTable: "t_e_commodite_com",
                        principalColumn: "com_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_typechambrecommodite_tpc_t_e_typechambre_tpc_tpc_id",
                        column: x => x.tpc_id,
                        principalTable: "t_e_typechambre_tpc",
                        principalColumn: "tpc_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_j_typechambrepointfort_tpf",
                columns: table => new
                {
                    ptf_id = table.Column<int>(type: "integer", nullable: false),
                    tpc_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_j_typechambrepointfort_tpf", x => new { x.ptf_id, x.tpc_id });
                    table.ForeignKey(
                        name: "FK_t_j_typechambrepointfort_tpf_t_e_pointfort_ptf_ptf_id",
                        column: x => x.ptf_id,
                        principalTable: "t_e_pointfort_ptf",
                        principalColumn: "ptf_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_j_typechambrepointfort_tpf_t_e_typechambre_tpc_tpc_id",
                        column: x => x.tpc_id,
                        principalTable: "t_e_typechambre_tpc",
                        principalColumn: "tpc_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_e_activitecarte_aca_tac_id",
                table: "t_e_activitecarte_aca",
                column: "tac_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_activiteincluse_aci_tac_id",
                table: "t_e_activiteincluse_aci",
                column: "tac_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_autreparticipant_apt_cvl_civiliteid",
                table: "t_e_autreparticipant_apt",
                column: "cvl_civiliteid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_cli_clientid",
                table: "t_e_avis_avi",
                column: "cli_clientid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_pht_photoid",
                table: "t_e_avis_avi",
                column: "pht_photoid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_avis_avi_res_resortid",
                table: "t_e_avis_avi",
                column: "res_resortid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_bar_bar_pht_photoid",
                table: "t_e_bar_bar",
                column: "pht_photoid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_bar_bar_res_id",
                table: "t_e_bar_bar",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebanquaire_cba_cba_cryptogramme",
                table: "t_e_cartebanquaire_cba",
                column: "cba_cryptogramme",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebanquaire_cba_cba_numerocarte",
                table: "t_e_cartebanquaire_cba",
                column: "cba_numerocarte",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_cartebanquaire_cba_cli_clientid",
                table: "t_e_cartebanquaire_cba",
                column: "cli_clientid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_categorietypechambre_ctc_ctc_libelle",
                table: "t_e_categorietypechambre_ctc",
                column: "ctc_libelle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_civilite_cvl_cvl_libelle",
                table: "t_e_civilite_cvl",
                column: "cvl_libelle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cli_email",
                table: "t_e_client_cli",
                column: "cli_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cli_tel",
                table: "t_e_client_cli",
                column: "cli_tel",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_cvl_civiliteid",
                table: "t_e_client_cli",
                column: "cvl_civiliteid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_client_cli_pay_paysid",
                table: "t_e_client_cli",
                column: "pay_paysid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_commodite_com_com_nom",
                table: "t_e_commodite_com",
                column: "com_nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_domaineskiable_dms_pht_id",
                table: "t_e_domaineskiable_dms",
                column: "pht_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_payement_pyt_rsv_reservationid",
                table: "t_e_payement_pyt",
                column: "rsv_reservationid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_pointfort_ptf_ptf_libelle",
                table: "t_e_pointfort_ptf",
                column: "ptf_libelle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_rsv_cli_clientid",
                table: "t_e_reservation_rsv",
                column: "cli_clientid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_rsv_res_resortid",
                table: "t_e_reservation_rsv",
                column: "res_resortid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_reservation_rsv_tra_transportid",
                table: "t_e_reservation_rsv",
                column: "tra_transportid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_resort_res_dms_domaineid",
                table: "t_e_resort_res",
                column: "dms_domaineid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_resort_res_loc_localisationid",
                table: "t_e_resort_res",
                column: "loc_localisationid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_resort_res_res_moyenneavis",
                table: "t_e_resort_res",
                column: "res_moyenneavis");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_resort_res_res_nom",
                table: "t_e_resort_res",
                column: "res_nom",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_t_e_resort_res_slo_souslocalisationid",
                table: "t_e_resort_res",
                column: "slo_souslocalisationid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_restaurant_rst_pht_photoid",
                table: "t_e_restaurant_rst",
                column: "pht_photoid");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_restaurant_rst_res_id",
                table: "t_e_restaurant_rst",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_typeactivite_tac_pht_id",
                table: "t_e_typeactivite_tac",
                column: "pht_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_typechambre_tpc_ctc_id",
                table: "t_e_typechambre_tpc",
                column: "ctc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_e_typechambre_tpc_res_id",
                table: "t_e_typechambre_tpc",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activitecarteenfantincluseresort_cir_res_id",
                table: "t_j_activitecarteenfantincluseresort_cir",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activitecarteenfantreservation_cer_rsv_id",
                table: "t_j_activitecarteenfantreservation_cer",
                column: "rsv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activitecarteenfantresort_cer_res_id",
                table: "t_j_activitecarteenfantresort_cer",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activitecartereservation_acr_rsv_id",
                table: "t_j_activitecartereservation_acr",
                column: "rsv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activitecarteresort_acr_res_id",
                table: "t_j_activitecarteresort_acr",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activiteenfantcartetrancheage_act_tag_id",
                table: "t_j_activiteenfantcartetrancheage_act",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activiteenfantinclusetrancheage_ait_tag_id",
                table: "t_j_activiteenfantinclusetrancheage_ait",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_activiteincluseresort_air_res_id",
                table: "t_j_activiteincluseresort_air",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_localisationsouslocalisation_lsl_slo_id",
                table: "t_j_localisationsouslocalisation_lsl",
                column: "slo_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_regroupementclubresort_rcr_res_id",
                table: "t_j_regroupementclubresort_rcr",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_reservationautreparticipant_sap_rsv_id",
                table: "t_j_reservationautreparticipant_sap",
                column: "rsv_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_resortphoto_rph_res_id",
                table: "t_j_resortphoto_rph",
                column: "res_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_resortvideo_rvi_vid_id",
                table: "t_j_resortvideo_rvi",
                column: "vid_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_trancheagetypeactivite_tta_tag_id",
                table: "t_j_trancheagetypeactivite_tta",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_typechambrecommodite_tpc_tpc_id",
                table: "t_j_typechambrecommodite_tpc",
                column: "tpc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_typechambrepointfort_tpf_tpc_id",
                table: "t_j_typechambrepointfort_tpf",
                column: "tpc_id");

            migrationBuilder.CreateIndex(
                name: "IX_t_j_typeclubresort_tcr_tcl_id",
                table: "t_j_typeclubresort_tcr",
                column: "tcl_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_e_avis_avi");

            migrationBuilder.DropTable(
                name: "t_e_bar_bar");

            migrationBuilder.DropTable(
                name: "t_e_cartebanquaire_cba");

            migrationBuilder.DropTable(
                name: "t_e_payement_pyt");

            migrationBuilder.DropTable(
                name: "t_e_restaurant_rst");

            migrationBuilder.DropTable(
                name: "t_j_activitecarteenfantincluseresort_cir");

            migrationBuilder.DropTable(
                name: "t_j_activitecarteenfantreservation_cer");

            migrationBuilder.DropTable(
                name: "t_j_activitecarteenfantresort_cer");

            migrationBuilder.DropTable(
                name: "t_j_activitecartereservation_acr");

            migrationBuilder.DropTable(
                name: "t_j_activitecarteresort_acr");

            migrationBuilder.DropTable(
                name: "t_j_activiteenfantcartetrancheage_act");

            migrationBuilder.DropTable(
                name: "t_j_activiteenfantinclusetrancheage_ait");

            migrationBuilder.DropTable(
                name: "t_j_activiteincluseresort_air");

            migrationBuilder.DropTable(
                name: "t_j_localisationsouslocalisation_lsl");

            migrationBuilder.DropTable(
                name: "t_j_regroupementclubresort_rcr");

            migrationBuilder.DropTable(
                name: "t_j_reservationautreparticipant_sap");

            migrationBuilder.DropTable(
                name: "t_j_resortphoto_rph");

            migrationBuilder.DropTable(
                name: "t_j_resortvideo_rvi");

            migrationBuilder.DropTable(
                name: "t_j_trancheagetypeactivite_tta");

            migrationBuilder.DropTable(
                name: "t_j_typechambrecommodite_tpc");

            migrationBuilder.DropTable(
                name: "t_j_typechambrepointfort_tpf");

            migrationBuilder.DropTable(
                name: "t_j_typeclubresort_tcr");

            migrationBuilder.DropTable(
                name: "t_e_activitecarte_aca");

            migrationBuilder.DropTable(
                name: "t_e_activiteenfantcarte_aec");

            migrationBuilder.DropTable(
                name: "t_e_activiteenfantincluse_aei");

            migrationBuilder.DropTable(
                name: "t_e_activiteincluse_aci");

            migrationBuilder.DropTable(
                name: "t_e_regroupementclub_rcl");

            migrationBuilder.DropTable(
                name: "t_e_autreparticipant_apt");

            migrationBuilder.DropTable(
                name: "t_e_reservation_rsv");

            migrationBuilder.DropTable(
                name: "t_e_video_vid");

            migrationBuilder.DropTable(
                name: "t_e_trancheage_tag");

            migrationBuilder.DropTable(
                name: "t_e_commodite_com");

            migrationBuilder.DropTable(
                name: "t_e_pointfort_ptf");

            migrationBuilder.DropTable(
                name: "t_e_typechambre_tpc");

            migrationBuilder.DropTable(
                name: "t_e_typeclub_tcl");

            migrationBuilder.DropTable(
                name: "t_e_typeactivite_tac");

            migrationBuilder.DropTable(
                name: "t_e_client_cli");

            migrationBuilder.DropTable(
                name: "t_e_transport_tra");

            migrationBuilder.DropTable(
                name: "t_e_categorietypechambre_ctc");

            migrationBuilder.DropTable(
                name: "t_e_resort_res");

            migrationBuilder.DropTable(
                name: "t_e_civilite_cvl");

            migrationBuilder.DropTable(
                name: "t_e_pays_pay");

            migrationBuilder.DropTable(
                name: "t_e_domaineskiable_dms");

            migrationBuilder.DropTable(
                name: "t_e_localisation_loc");

            migrationBuilder.DropTable(
                name: "t_e_souslocalisation_slo");

            migrationBuilder.DropTable(
                name: "t_e_photo_pht");
        }
    }
}
