﻿using Microsoft.EntityFrameworkCore;
using Surfs_Up_API.Models;

namespace Surfs_Up_API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context.Surfboards.Any())
                {
                    return;   // DB has been seeded
                }
                context.Surfboards.AddRange(
                    new Surfboard
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        Type = BOARDTYPE.Shortboard,
                        Price = 565,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardShortboard.png",
                        Description = "Perfekt for hurtige vendinger og manøvrering. Udforsk vores sortiment af shortboards her"
                    },
                    new Surfboard
                    {
                        Name = "The Wide Glider",
                        Length = 7.1,
                        Width = 21.75,
                        Thickness = 2.75,
                        Volume = 44.16,
                        Type = BOARDTYPE.Funboard,
                        Price = 685,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardFunboard.webp",
                        Description = "Skør og sjovt board til alle aldre. Udforsk vores sortiment af funboards her"
                    },
                    new Surfboard
                    {
                        Name = "The Golden Ration",
                        Length = 6.3,
                        Width = 21.85,
                        Thickness = 2,
                        Volume = 43.22,
                        Type = BOARDTYPE.Funboard,
                        Price = 695,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardFunboard.webp",
                        Description = "Skør og sjovt board til alle aldre. Udforsk vores sortiment af funboards her"
                    },
                    new Surfboard
                    {
                        Name = "Mahi Mahi",
                        Length = 5.4,
                        Width = 20.75,
                        Thickness = 2.3,
                        Volume = 29.39,
                        Type = BOARDTYPE.Fish,
                        Price = 645,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardFish.png",
                        Description = "Lille og agil!. Udforsk vores sortiment af fish boards her"
                    },
                    new Surfboard
                    {
                        Name = "The Emerald Glider",
                        Length = 9.2,
                        Width = 22.8,
                        Thickness = 2.8,
                        Volume = 65.4,
                        Type = BOARDTYPE.Longboard,
                        Price = 895,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardLongboard.png",
                        Description = "Langt og hurtigt. Udforsk vores sortiment af longboards her"
                    },
                    new Surfboard
                    {
                        Name = "The Bomb",
                        Length = 5.5,
                        Width = 21,
                        Thickness = 2.5,
                        Volume = 33.7,
                        Type = BOARDTYPE.Shortboard,
                        Price = 645,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardShortboard.png",
                        Description = "Perfekt for hurtige vendinger og manøvrering. Udforsk vores sortiment af shortboards her"
                    },
                    new Surfboard
                    {
                        Name = "Walden Magic",
                        Length = 9.6,
                        Width = 19.4,
                        Thickness = 3,
                        Volume = 80,
                        Type = BOARDTYPE.Longboard,
                        Price = 1025,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardLongboard.png",
                        Description = "Langt og hurtigt. Udforsk vores sortiment af longboards her"
                    },
                    new Surfboard
                    {
                        Name = "Naish One",
                        Length = 12.6,
                        Width = 30,
                        Thickness = 6,
                        Volume = 301,
                        Type = BOARDTYPE.SUP,
                        Price = 854,
                        Equipment = "Paddle",
                        ImagePath = "~/images/SurfboardLongboard.png",
                        Description = "Hvad er en SUP?. Udforsk vores sortiment af SUPs her"
                    },
                    new Surfboard
                    {
                        Name = "Six Tourer",
                        Length = 11.6,
                        Width = 32,
                        Thickness = 6,
                        Volume = 270,
                        Type = BOARDTYPE.SUP,
                        Price = 611,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardLongboard.png",
                        Description = "Hvad er en SUP?. Udforsk vores sortiment af SUPs her"
                    },
                    new Surfboard
                    {
                        Name = "Naish Maliko",
                        Length = 14,
                        Width = 25,
                        Thickness = 6,
                        Volume = 330,
                        Type = BOARDTYPE.SUP,
                        Price = 1304,
                        Equipment = "Fin, Paddle, Pump, Leash",
                        ImagePath = "~/images/SurfboardLongboard.png",
                        Description = "Hvad er en SUP?. Udforsk vores sortiment af SUPs her"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
