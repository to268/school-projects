using System;
using System.Collections.Generic;

namespace DeeVizion.MVVM.Model
{
    public interface Crud<T>
    {
        /// <summary>
        /// Cet fonction sert � inser� des donn�es dans la base de donn�es
        /// </summary>
        void Create();
        /// <summary>
        /// Cet fonction sert � acced� au donn�es contenu dans la base de donn�es et les recup�rer dans le logiciel
        /// </summary>
        void Read();
        /// <summary>
        /// Cet fonction sert � supprimer des donn�es dans la base de donn�es
        /// </summary>
        void Delete();
        /// <summary>
        /// Cet fonction sert � mettre � jour les donn�es dans la base de donn�es
        /// </summary>
        void Update();
        /// <summary>
        /// Cet fonction sert � cherch� toute les occurence dans une table donn�es de la base de donn�es
        /// </summary>
        /// <returns>List<T></returns>
        List<T> FindAll();
        /// <summary>
        /// Cet fonction sert � cherch� toute les occurence d'un libell� dans une table donn�es de la base de donn�es
        /// </summary>
        /// <param name="criteres"> Ce qui est recherch�</param>
        /// <returns>List<T></returns>
        List<T> FindBySelection(String criteres);
    }
}
