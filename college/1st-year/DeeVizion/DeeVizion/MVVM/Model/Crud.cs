using System;
using System.Collections.Generic;

namespace DeeVizion.MVVM.Model
{
    public interface Crud<T>
    {
        /// <summary>
        /// Cet fonction sert à inseré des données dans la base de données
        /// </summary>
        void Create();
        /// <summary>
        /// Cet fonction sert à accedé au données contenu dans la base de données et les recupérer dans le logiciel
        /// </summary>
        void Read();
        /// <summary>
        /// Cet fonction sert à supprimer des données dans la base de données
        /// </summary>
        void Delete();
        /// <summary>
        /// Cet fonction sert à mettre à jour les données dans la base de données
        /// </summary>
        void Update();
        /// <summary>
        /// Cet fonction sert à cherché toute les occurence dans une table données de la base de données
        /// </summary>
        /// <returns>List<T></returns>
        List<T> FindAll();
        /// <summary>
        /// Cet fonction sert à cherché toute les occurence d'un libellé dans une table données de la base de données
        /// </summary>
        /// <param name="criteres"> Ce qui est recherché</param>
        /// <returns>List<T></returns>
        List<T> FindBySelection(String criteres);
    }
}
