using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ApiClubMed.Models.EntityFramework
{
    public partial class ClubMedDBContexts : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public virtual DbSet<ActiviteEnfantCarte> ActiviteEnfantCartes { get; set; } = null!;
        public virtual DbSet<ActiviteCarte> ActiviteCartes { get; set; } = null!;
        public virtual DbSet<ActiviteEnfantIncluse> ActiviteEnfantIncluses { get; set; } = null!;
        public virtual DbSet<DomaineSkiable> DomaineSkiables { get; set; } = null!;
        public virtual DbSet<Localisation> Localisations { get; set; } = null!;
        public virtual DbSet<RegroupementClub> RegroupementClubs { get; set; } = null!;
        public virtual DbSet<SousLocalisation> SousLocalisations { get; set; } = null!;
        public virtual DbSet<TypeClub> TypeClubs { get; set; } = null!;
        public virtual DbSet<TrancheAge> TrancheAges { get; set; } = null!;
        public virtual DbSet<ActiviteIncluse> ActiviteIncluses { get; set; } = null!;
        public virtual DbSet<TypeActivite> TypeActivites { get; set; } = null!;
        public virtual DbSet<AutreParticipant> AutreParticipants { get; set; } = null!;
        public virtual DbSet<Avis> Avis { get; set; } = null!;
        public virtual DbSet<CarteBanquaire> CarteBanquaires { get; set; } = null!;
        public virtual DbSet<Civilite> Civilites { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Payement> Payements { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Resort> Resorts { get; set; } = null!;
        public virtual DbSet<Bar> Bars { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<CategorieTypeChambre> CategorieTypeChambres { get; set; } = null!;
        public virtual DbSet<TypeChambre> TypeChambres { get; set; } = null!;
        public virtual DbSet<PointFort> PointForts { get; set; } = null!;
        public virtual DbSet<Commodite> Commodites { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;
        public virtual DbSet<Pays> Pays { get; set; } = null!;
        public virtual DbSet<Transport> Transports { get; set; } = null!;


        public ClubMedDBContexts()
        {

        }

        public ClubMedDBContexts(DbContextOptions<ClubMedDBContexts> options) : base(options)
        {

        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                /*=> */
            //                optionsBuilder.UseLoggerFactory(MyLoggerFactory)
            //             .EnableSensitiveDataLogging()
            //             .UseNpgsql("Server=localhost;port=5432;Database=FilmsDB;uid = postgres; password = postgres; ");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SamuelModel(modelBuilder);
            DimitriModel(modelBuilder);
            HugoModel(modelBuilder);
            TonyModel(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private void SamuelModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeClub>(entity =>
            {
                entity.HasKey(e => e.TypeClubId)
                    .HasName("pk_typeclub");

                entity.HasMany(d => d.LesResorts)
                    .WithMany(p => p.LesTypeClubs)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_typeclubresort_tcr",
                        j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"),
                        j => j.HasOne<TypeClub>().WithMany().HasForeignKey("tcl_id"));
            });

            modelBuilder.Entity<RegroupementClub>(entity =>
            {
                entity.HasKey(e => e.RegroupementClubId)
                    .HasName("pk_regroupementclub");

                entity.HasMany(d => d.LesResorts)
                    .WithMany(p => p.LesRegroupementClubs)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_regroupementclubresort_rcr",
                        j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"),
                        j => j.HasOne<RegroupementClub>().WithMany().HasForeignKey("rcl_id"));
            });


            modelBuilder.Entity<DomaineSkiable>(entity =>
            {
                entity.HasKey(e => e.DomaineSkiableId)
                    .HasName("pk_domaineskiable");


                entity.HasOne(d => d.LaPhoto)
                    .WithMany(p => p.LesDomaineSkiables)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("pht_id");
            });


            modelBuilder.Entity<SousLocalisation>(entity =>
            {
                entity.HasKey(e => e.SousLocalisationId)
                    .HasName("pk_souslocalisation");
            });

            modelBuilder.Entity<Localisation>(entity =>
            {
                entity.HasKey(e => e.LocalisationId)
                    .HasName("pk_localisation");

                entity.HasMany(d => d.LesSousLocalisations)
                    .WithMany(p => p.LesLocalisations)
                    .UsingEntity(j => j.ToTable("Test"));

                entity.HasMany(d => d.LesSousLocalisations)
                    .WithMany(p => p.LesLocalisations)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_localisationsouslocalisation_lsl",
                        j => j.HasOne<SousLocalisation>().WithMany().HasForeignKey("slo_id"),
                        j => j.HasOne<Localisation>().WithMany().HasForeignKey("loc_id"));
            });

            modelBuilder.Entity<ActiviteEnfantCarte>(entity =>
            {
                entity.HasKey(e => e.ActiviteEnfantCarteId)
                    .HasName("pk_activiteenfantcarte");

                entity.HasMany(d => d.LesResorts)
                    .WithMany(p => p.LesActiviteEnfantCartes)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activitecarteenfantresort_cer",
                        j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"),
                        j => j.HasOne<ActiviteEnfantCarte>().WithMany().HasForeignKey("aec_id"));

                entity.HasMany(d => d.LesReservations)
                    .WithMany(p => p.LesActiviteEnfantCartes)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activitecarteenfantreservation_cer",
                        j => j.HasOne<Reservation>().WithMany().HasForeignKey("rsv_id"),
                        j => j.HasOne<ActiviteEnfantCarte>().WithMany().HasForeignKey("aec_id"));
            });

            modelBuilder.Entity<ActiviteEnfantIncluse>(entity =>
            {
                entity.HasKey(e => e.ActiviteEnfantIncluseId)
                    .HasName("pk_activiteenfantincluse");

                entity.HasMany(d => d.LesResorts)
                    .WithMany(p => p.LesActiviteEnfantIncluses)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activitecarteenfantincluseresort_cir",
                        j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"),
                        j => j.HasOne<ActiviteEnfantIncluse>().WithMany().HasForeignKey("aei_id"));
            });
        }

        private void HugoModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutreParticipant>(entity =>
            {
                // autreparticipant id
                entity.HasKey(e => e.AutreParticipantId)
                    .HasName("pk_autreparticipant");

                entity.HasCheckConstraint("ck_apt_datenaissance", "apt_datenaissance < current_date");

                entity.HasOne(d => d.LaCivilite)
                    .WithMany(p => p.LesAutreParticipants)
                    .HasForeignKey(d => d.CiviliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_apt_cvl");

                entity.Property(b => b.DateNaissance).HasDefaultValueSql("CURRENT_DATE");
            });

            modelBuilder.Entity<Avis>(entity =>
            {
                // avis id
                entity.HasKey(e => e.AvisId)
                    .HasName("pk_avis");

                entity.HasCheckConstraint("ck_avi_note", "avi_note between 0 and 5");

                entity.Property(b => b.Date).HasDefaultValueSql("CURRENT_DATE");

                entity.HasOne(d => d.LeResort)
                    .WithMany(p => p.LesAvis)
                    .HasForeignKey(d => d.ResortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avi_res");

                entity.Property(p => p.PhotoId).IsRequired(false);

                // client
                entity.HasOne(d => d.LeClient)
                    .WithMany(p => p.LesAvis)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avi_cli");
            });

            // CarteBanquaire
            modelBuilder.Entity<CarteBanquaire>(entity =>
            {
                // cartebanquaire id
                entity.HasKey(e => e.CarteBanquaireId)
                    .HasName("pk_cartebanquaire");

                entity.HasCheckConstraint("ck_cba_dateexpiration", "cba_dateexpiration > CURRENT_DATE");

                // date expiration
                entity.Property(b => b.DateExpiration).HasDefaultValueSql("CURRENT_DATE");
                // unique numerocarte
                entity.HasIndex(e => e.NumeroCarte).IsUnique();
                // unique cryptogramme
                entity.HasIndex(e => e.Cryptogramme).IsUnique();

                entity.HasOne(d => d.LeClient)
                    .WithMany(p => p.LesCarteBanquaires)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cba_cli");
            });

            // Civilite
            modelBuilder.Entity<Civilite>(entity =>
            {
                entity.HasKey(e => e.CiviliteId)
                    .HasName("pk_civilite");

                entity.HasCheckConstraint("ck_cvl_libelle", "cvl_libelle in ('Monsieur', 'Madame', 'Mademoiselle', 'Anonyme')");

                entity.HasIndex(e => e.Libelle).IsUnique();

                entity.Property(b => b.Libelle).HasDefaultValue("Monsieur");
            });

            // Client
            modelBuilder.Entity<Client>(entity =>
            {
                // client id
                entity.HasKey(e => e.ClientId)
                    .HasName("pk_client");

                entity.HasCheckConstraint("ck_cli_datenaissance", "cli_datenaissance < current_date");

                entity.HasOne(d => d.LaCivilite)
                    .WithMany(p => p.LesClients)
                    .HasForeignKey(d => d.CiviliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cli_cvl");

                // pays
                entity.HasOne(d => d.LePays)
                    .WithMany(p => p.LesClients)
                    .HasForeignKey(d => d.PaysId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cli_pys");

                entity.Property(b => b.DateNaissance).HasDefaultValueSql("CURRENT_DATE");

                // email
                entity.HasIndex(e => e.Email).IsUnique();

                // telephone
                entity.HasIndex(e => e.Tel).IsUnique();

                // regular expression email
                entity.HasCheckConstraint(
                    "ck_cli_email",
                    "cli_email ~* '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}$'"
                );

                // default value tempsrestant
                entity.Property(b => b.TempsRestant).HasDefaultValue("0 ans 0 mois 0 jours");

                // La civilite
                entity.HasOne(d => d.LaCivilite)
                    .WithMany(p => p.LesClients)
                    .HasForeignKey(d => d.CiviliteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cli_cvl");

                // Le pays
                entity.HasOne(d => d.LePays)
                    .WithMany(p => p.LesClients)
                    .HasForeignKey(d => d.PaysId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cli_pys");
            });

            // payement
            modelBuilder.Entity<Payement>(entity =>
            {
                // payement id
                entity.HasKey(e => e.PayementId)
                    .HasName("pk_payement");

                entity.HasCheckConstraint("ck_pyt_montant", "pyt_montant > 0");

                entity.HasOne(d => d.LaReservation)
                    .WithMany(p => p.LesPayements)
                    .HasForeignKey(d => d.ReservationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pyt_rsv");
            });

            // reservation
            modelBuilder.Entity<Reservation>(entity =>
            {
                // reservation id
                entity.HasKey(e => e.ReservationId)
                    .HasName("pk_reservation");

                entity.HasCheckConstraint("ck_rsv_datefin", "rsv_datefin > rsv_datedebut");
                entity.HasCheckConstraint("ck_rsv_prix", "rsv_prix > 0");

                entity.Property(b => b.DateDebut).HasDefaultValueSql("CURRENT_DATE");
                entity.Property(b => b.DateFin).HasDefaultValueSql("CURRENT_DATE + 1");
                entity.Property(b => b.Prix).HasDefaultValue(0);
                entity.Property(b => b.Validation).HasDefaultValue(false);
                entity.Property(b => b.Confirmation).HasDefaultValue(false);

                entity.HasOne(d => d.LeClient)
                    .WithMany(p => p.LesReservations)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rsv_cli");

                entity.HasOne(d => d.LeTransport)
                    .WithMany(p => p.LesReservations)
                    .HasForeignKey(d => d.TransportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rsv_tra");

                // le resort
                entity.HasOne(d => d.LeResort)
                    .WithMany(p => p.LesReservations)
                    .HasForeignKey(d => d.ResortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rsv_res");


                // autre participant
                entity.HasMany(d => d.LesAutreParticipants)
                .WithMany(p => p.LesReservations)
                .UsingEntity<Dictionary<string, object>>(
                    "t_j_reservationautreparticipant_sap",
                    j => j.HasOne<AutreParticipant>().WithMany().HasForeignKey("apt_id"),
                    j => j.HasOne<Reservation>().WithMany().HasForeignKey("rsv_id"));

            });

            // Transport
            modelBuilder.Entity<Transport>(entity =>
            {
                entity.HasKey(e => e.TransportId)
                    .HasName("pk_transport");
            });

            // resort
            modelBuilder.Entity<Resort>(entity =>
            {
                entity.HasKey(e => e.ResortId)
                    .HasName("pk_resort");

                // nom
                entity.HasIndex(e => e.Nom).IsUnique();

                entity.HasCheckConstraint("ck_res_moyenneavis", "res_moyenneavis >= 0 AND res_moyenneavis <= 5");
                entity.HasIndex(e => e.MoyenneAvis);

                entity.HasCheckConstraint("ck_res_prixdepart", "res_prixdepart > 0");

                // prix depart default 0
                entity.Property(b => b.PrixDepart).HasDefaultValue(0);

                entity.HasMany(d => d.LesPhotos)
                   .WithMany(p => p.LesResorts)
                   .UsingEntity<Dictionary<string, object>>(
                       "t_j_resortphoto_rph",
                       j => j.HasOne<Photo>().WithMany().HasForeignKey("pht_id"),
                       j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"));


                entity.HasMany(d => d.LesVideos)
                   .WithMany(p => p.LesResorts)
                   .UsingEntity<Dictionary<string, object>>(
                       "t_j_resortvideo_rvi",
                       j => j.HasOne<Video>().WithMany().HasForeignKey("vid_id"),
                       j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"));

                // le domaine skiable
                entity.HasOne(d => d.LeDomaineSkiable)
                    .WithMany(p => p.LesResorts)
                    .HasForeignKey(d => d.DomaineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_res_dom");

                // La localisation
                entity.HasOne(d => d.LaLocalisation)
                    .WithMany(p => p.LesResorts)
                    .HasForeignKey(d => d.LocalisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_res_loc");

                // la sous localisation
                entity.HasOne(d => d.LaSousLocalisation)
                    .WithMany(p => p.LesResorts)
                    .HasForeignKey(d => d.SouslocalisationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_res_slo");



            });
        }

        private void DimitriModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bar>(entity =>
            {
                entity.HasKey(e => e.BarId).HasName("pk_bar");



                entity.HasOne(d => d.LaPhoto)
                    .WithMany(p => p.LesBars)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_bar_vid");

                entity.HasOne(d => d.LeResort)
                    .WithMany(p => p.LesBars)
                    .HasForeignKey(d => d.ResortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_bar_res");

            });


            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasKey(e => e.RestaurantId).HasName("pk_restaurant");



                entity.HasOne(d => d.LaPhoto)
                    .WithMany(p => p.LesRestaurants)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rst_vid");

                entity.HasOne(d => d.LeResort)
                    .WithMany(p => p.LesRestaurants)
                    .HasForeignKey(d => d.ResortId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rst_res");
            });

            modelBuilder.Entity<CategorieTypeChambre>(entity =>
            {
                entity.HasKey(e => e.CategorieTypeChambreId).HasName("pk_categorietypechambre");



                entity.HasMany(d => d.LesTypeChambres)
                    .WithOne(p => p.LaCategorieTypeChambre)
                    .HasForeignKey(d => d.TypeChambreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ctc_tpc");

                entity.HasIndex(e => e.Libelle).IsUnique();

            });

            modelBuilder.Entity<TypeChambre>(entity =>
            {
                entity.HasKey(e => e.TypeChambreId)
                    .HasName("pk_typechambre");


                entity.HasCheckConstraint("ck_tpc_surface", "tpc_surface > 0 ");
                entity.HasCheckConstraint("ck_tpc_capacite", "tpc_capacite > 0 ");
                entity.HasCheckConstraint("ck_tpc_prixjournalier", "tpc_prixjournalier > 0 ");

                entity.HasOne(d => d.LaCategorieTypeChambre)
                   .WithMany(p => p.LesTypeChambres)
                   .HasForeignKey(d => d.CategorieTypeChambreId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_tpc_ctc");

                entity.HasMany(d => d.LesPointForts)
                   .WithMany(p => p.LesTypeChambres)
                   .UsingEntity<Dictionary<string, object>>(
                       "t_j_typechambrepointfort_tpf",
                       j => j.HasOne<PointFort>().WithMany().HasForeignKey("ptf_id"),
                       j => j.HasOne<TypeChambre>().WithMany().HasForeignKey("tpc_id"));

                entity.HasMany(d => d.LesCommodites)
                .WithMany(p => p.LesTypeChambres)
                .UsingEntity<Dictionary<string, object>>(
                    "t_j_typechambrecommodite_tpc",
                    j => j.HasOne<Commodite>().WithMany().HasForeignKey("com_id"),
                    j => j.HasOne<TypeChambre>().WithMany().HasForeignKey("tpc_id"));



                entity.HasOne(d => d.LeResort)
                 .WithMany(p => p.LesTypeChambres)
                 .HasForeignKey(d => d.ResortId)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("fk_tpc_res");

            });

            modelBuilder.Entity<PointFort>(entity =>
            {
                entity.HasKey(e => e.PointFortId).HasName("pk_pointfort");
                entity.HasIndex(e => e.Libelle).IsUnique();

            });

            modelBuilder.Entity<Commodite>(entity =>
            {
                entity.HasKey(e => e.CommoditeId).HasName("pk_commodite");
                entity.HasIndex(e => e.Nom).IsUnique();

            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.HasKey(e => e.VideoId).HasName("pk_video");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasKey(e => e.PhotoId)
                    .HasName("pk_photo");



                entity.HasMany(d => d.LesBars)
                   .WithOne(p => p.LaPhoto)
                   .HasForeignKey(d => d.PhotoId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_pht_bar");

                entity.HasMany(d => d.LesRestaurants)
                    .WithOne(p => p.LaPhoto)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_pht_rst");

                entity.HasMany(d => d.LesTypeActivites)
                   .WithOne(p => p.LaPhoto)
                   .HasForeignKey(d => d.PhotoId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_pht_tac");

                entity.HasMany(d => d.LesAvis)
                   .WithOne(p => p.LaPhoto)
                   .HasForeignKey(d => d.PhotoId)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("fk_pht_avi");

                entity.HasMany(d => d.LesDomaineSkiables)
                  .WithOne(p => p.LaPhoto)
                  .HasForeignKey(d => d.PhotoId)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("fk_pht_dms");

            });
        }
        private void TonyModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeActivite>(entity =>
            {
                entity.HasKey(e => e.TypeActiviteId)
                    .HasName("pk_typeactivite");

                entity.HasMany(d => d.LesTrancheAges)
                    .WithMany(p => p.LesTypeActivites)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_trancheagetypeactivite_tta",
                        j => j.HasOne<TrancheAge>().WithMany().HasForeignKey("tag_id"),
                        j => j.HasOne<TypeActivite>().WithMany().HasForeignKey("tac_id"));
            });

            modelBuilder.Entity<TrancheAge>(entity =>
            {
                entity.HasKey(e => e.TrancheAgeId)
                    .HasName("pk_trancheage");

                entity.HasMany(d => d.LesActiviteEnfantCartes)
                    .WithMany(p => p.LesTrancheAges)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activiteenfantcartetrancheage_act",
                        j => j.HasOne<ActiviteEnfantCarte>().WithMany().HasForeignKey("aec_id"),
                        j => j.HasOne<TrancheAge>().WithMany().HasForeignKey("tag_id"));

                entity.HasMany(d => d.LesActiviteEnfantIncluses)
                    .WithMany(p => p.LesTrancheAges)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activiteenfantinclusetrancheage_ait",
                        j => j.HasOne<ActiviteEnfantIncluse>().WithMany().HasForeignKey("aei_id"),
                        j => j.HasOne<TrancheAge>().WithMany().HasForeignKey("tag_id"));
            });

            modelBuilder.Entity<ActiviteCarte>(entity =>
            {
                entity.HasKey(e => e.ActiviteCarteId)
                    .HasName("pk_activitecarte");

                entity.HasMany(d => d.LesReservations)
                    .WithMany(p => p.LesActiviteCartes)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activitecartereservation_acr",
                        j => j.HasOne<Reservation>().WithMany().HasForeignKey("rsv_id"),
                        j => j.HasOne<ActiviteCarte>().WithMany().HasForeignKey("aca_id"));

                entity.HasMany(d => d.LesResorts)
                    .WithMany(p => p.LesActiviteCartes)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activitecarteresort_acr",
                        j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"),
                        j => j.HasOne<ActiviteCarte>().WithMany().HasForeignKey("aca_id"));
            });

            modelBuilder.Entity<ActiviteIncluse>(entity =>
            {
                entity.HasKey(e => e.ActiviteIncluseId)
                    .HasName("pk_activiteincluse");

                entity.HasMany(d => d.LesResorts)
                    .WithMany(p => p.LesActiviteIncluses)
                    .UsingEntity<Dictionary<string, object>>(
                        "t_j_activiteincluseresort_air",
                        j => j.HasOne<Resort>().WithMany().HasForeignKey("res_id"),
                        j => j.HasOne<ActiviteIncluse>().WithMany().HasForeignKey("aci_id"));
            });
        }
    }
}
