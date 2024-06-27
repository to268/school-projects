class Medication {
    constructor({
      name = '',
      genericName = '',
      medicationCode = '',
      form = '',
      dosage = '',
      frequency = '',
      duration = '',
      administrationInstructions = '',
      sideEffects = '',
      contraindications = '',
      manufacturer = '',
      lotNumber = '',
      expirationDate = '',
      therapeuticClass = '',
      stockAvailable = 0,
      price = 0,
      additionalNotes = ''
    } = {}) {
      this._name = name;
      this._genericName = genericName;
      this._medicationCode = medicationCode;
      this._form = form;
      this._dosage = dosage;
      this._frequency = frequency;
      this._duration = duration;
      this._administrationInstructions = administrationInstructions;
      this._sideEffects = sideEffects;
      this._contraindications = contraindications;
      this._manufacturer = manufacturer;
      this._lotNumber = lotNumber;
      this._expirationDate = expirationDate;
      this._therapeuticClass = therapeuticClass;
      this._stockAvailable = stockAvailable;
      this._price = price;
      this._additionalNotes = additionalNotes;
    }
  
    // Getters
    get name() {
      return this._name;
    }
  
    get genericName() {
      return this._genericName;
    }
  
    get medicationCode() {
      return this._medicationCode;
    }
  
    get form() {
      return this._form;
    }
  
    get dosage() {
      return this._dosage;
    }
  
    get frequency() {
      return this._frequency;
    }
  
    get duration() {
      return this._duration;
    }
  
    get administrationInstructions() {
      return this._administrationInstructions;
    }
  
    get sideEffects() {
      return this._sideEffects;
    }
  
    get contraindications() {
      return this._contraindications;
    }
  
    get manufacturer() {
      return this._manufacturer;
    }
  
    get lotNumber() {
      return this._lotNumber;
    }
  
    get expirationDate() {
      return this._expirationDate;
    }
  
    get therapeuticClass() {
      return this._therapeuticClass;
    }
  
    get stockAvailable() {
      return this._stockAvailable;
    }
  
    get price() {
      return this._price;
    }
  
    get additionalNotes() {
      return this._additionalNotes;
    }
  
    // Setters
    set name(value) {
      this._name = value;
    }
  
    set genericName(value) {
      this._genericName = value;
    }
  
    set medicationCode(value) {
      this._medicationCode = value;
    }
  
    set form(value) {
      this._form = value;
    }
  
    set dosage(value) {
      this._dosage = value;
    }
  
    set frequency(value) {
      this._frequency = value;
    }
  
    set duration(value) {
      this._duration = value;
    }
  
    set administrationInstructions(value) {
      this._administrationInstructions = value;
    }
  
    set sideEffects(value) {
      this._sideEffects = value;
    }
  
    set contraindications(value) {
      this._contraindications = value;
    }
  
    set manufacturer(value) {
      this._manufacturer = value;
    }
  
    set lotNumber(value) {
      this._lotNumber = value;
    }
  
    set expirationDate(value) {
      this._expirationDate = value;
    }
  
    set therapeuticClass(value) {
      this._therapeuticClass = value;
    }
  
    set stockAvailable(value) {
      this._stockAvailable = value;
    }
  
    set price(value) {
      this._price = value;
    }
  
    set additionalNotes(value) {
      this._additionalNotes = value;
    }
  }
  
  export { Medication };
  