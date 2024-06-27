<template>
    <div id="modal_event"
        class="bg-black/30 backdrop-blur absolute z-50 p-3 w-full h-full absolute flex items-center justify-center absolute">
        <div class="bg-white rounded-lg  w-5/6 ">
            <div class="grid grid-cols-2">
                <div class="border-b h-20 text-black text-4xl flex p-3">
                    <h1> Ajout d'un rendez-vous</h1>
                </div>
                <div class="border-b h-20 text-black text-4xl flex justify-end">
                    <i class="bi bi-x p-5 pointer-event cursor-pointer" id="btn-close"></i>
                </div>
            </div>
            <div class="grid grid-cols-3 w-full text-black h-fit p-4">
                <div>
                    <span class="p-4  font-medium rounded-l-lg">Date</span>
                    <div class="flex pl-4  items-center w-full">

                        <span class="p-3 bg-gray-200 rounded-l-lg"><i class="bi bi-calendar2-plus"></i></span>
                        <input id="input_date_range" type="text" value="" required
                            class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-r-lg font-medium w-full text-center">
                    </div>
                </div>
                <div class="w-full">
                    <span class="p-4 font-medium rounded-l-lg ">Heure de début</span>
                    <div class="flex pl-4 pr-4  items-center w-full">

                        <input id="heure_debut" type="time" value="" required
                            class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-lg font-medium w-full text-center">
                    </div>
                </div>
                <div class="w-full">
                    <span class="p-4 font-medium rounded-l-lg ">Heure de fin</span>
                    <div class="flex pl-4 pr-4  items-center w-full">

                        <input id="heure_fin" type="time" value="" required
                            class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-lg font-medium w-full text-center">
                    </div>
                </div>
                <div class="w-full">
                    <span class="p-4  font-medium rounded-l-lg">Raison</span>
                    <div class="flex pl-4  items-center w-full">

                        <input id="titre_evenement" type="text" value="" required
                            class="caret-blue-500 focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-lg font-medium w-full">
                    </div>
                </div>
                <!-- <div class="w-full">
                    <span class="p-4  font-medium rounded-l-lg">Prix (€)</span> 
                    <div class="flex pl-4  items-center w-full">

                        <input id="prix" type="number"  class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-lg font-medium w-full text-center">
                    </div>
                </div> -->
                <div class=" w-full">
                    <span class="p-4  font-medium rounded-l-lg">Docteur</span>
                    <div class="flex pl-4  items-center w-full">

                        <select id="select_docteurs" value="" required
                            class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-lg font-medium w-full">
                            <option disabled selected>Veuillez choisir un docteur</option>

                        </select>
                    </div>
                </div>
                <div class=" w-full">
                    <span class="p-4  font-medium rounded-l-lg">Patient</span>
                    <div class="flex pl-4  items-center w-full">

                        <select id="select_patients" value="" required
                            class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 bg-gray-200 rounded-lg font-medium w-full">
                            <option disabled selected>Veuillez choisir un patient</option>

                        </select>
                    </div>
                </div>

                <div class=" w-full place-self-center">
                    <span class="p-4  font-medium rounded-l-lg">Couleur</span>
                    <div class="flex pl-4  items-center w-full">
                        <input id="couleur_evenement" type="color" value=""
                            class="focus:ring-offset-2 focus:outline-none  focus:ring-1 focus:ring-sky-700 p-3 h-12 rounded-lg font-medium w-1/3 cursor-pointer">
                    </div>
                </div>
                <div class=" col-span-3 mt-3 pl-4">
                    <span class="  font-medium rounded-l-lg">Description</span>

                    <div id="description" class="h-fit w-full">
                    </div>
                </div>
                <div class="place-self-end col-span-3">
                    <div class="flex pt-4 pl-4  items-center w-1/6">
                        <button id="btn_save"
                            class="p-3 bg-green-700 rounded-lg text-white hover:bg-green-800">Enregistrer
                        </button>
                    </div>
                </div>
            </div>

        </div>
    </div>
</template>