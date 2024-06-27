// Sous-classes pour les schémas
class Disease {
  constructor({
    name = '',
    diagnosisDate = new Date(),
    details = ''
  } = {}) {
    this.name = name;
    this.diagnosisDate = diagnosisDate;
    this.details = details;
  }

  toJson() {
    return {
      name: this.name,
      diagnosisDate: this.diagnosisDate,
      details: this.details
    };
  }
}

class Allergy {
  constructor({
    allergy = '',
    reaction = '',
    severity = 0
  } = {}) {
    this.allergy = allergy;
    this.reaction = reaction;
    this.severity = severity;
  }

  toJson() {
    return {
      allergy: this.allergy,
      reaction: this.reaction,
      severity: this.severity
    };
  }
}

class Surgery {
  constructor({
    type = '',
    date = new Date(),
    details = ''
  } = {}) {
    this.type = type;
    this.date = date;
    this.details = details;
  }

  toJson() {
    return {
      type: this.type,
      date: this.date,
      details: this.details
    };
  }
}

class Diagnostic {
  constructor({
    diagnostic = '',
    date = new Date(),
    treatment = '',
    notes = ''
  } = {}) {
    this.diagnostic = diagnostic;
    this.date = date;
    this.treatment = treatment;
    this.notes = notes;
  }

  toJson() {
    return {
      diagnostic: this.diagnostic,
      date: this.date,
      treatment: this.treatment,
      notes: this.notes
    };
  }
}

// Classe principale pour MedicalProfile
class MedicalProfile {
  constructor({
    patient = '', // ID du patient
    diseases = [],
    allergies = [],
    surgery = [],
    diagnostics = [],
    health = 0,
    notes = '',
    bloodType = '',
    weight = 0,
    height = 0,
    famillyHistory = '',
    lastUpdate = new Date()
  } = {}) {
    this._patient = patient;
    this._diseases = diseases.map(item => new Disease(item));
    this._allergies = allergies.map(item => new Allergy(item));
    this._surgery = surgery.map(item => new Surgery(item));
    this._diagnostics = diagnostics.map(item => new Diagnostic(item));
    this._health = health;
    this._notes = notes;
    this._bloodType = bloodType;
    this._weight = weight;
    this._height = height;
    this._famillyHistory = famillyHistory;
    this._lastUpdate = lastUpdate;
  }

  // Getters
  get patient() {
    return this._patient;
  }

  get diseases() {
    return this._diseases;
  }

  get allergies() {
    return this._allergies;
  }

  get surgery() {
    return this._surgery;
  }

  get diagnostics() {
    return this._diagnostics;
  }

  get health() {
    return this._health;
  }

  get notes() {
    return this._notes;
  }

  get bloodType() {
    return this._bloodType;
  }

  get weight() {
    return this._weight;
  }

  get height() {
    return this._height;
  }

  get famillyHistory() {
    return this._famillyHistory;
  }

  get lastUpdate() {
    return this._lastUpdate;
  }

  // Setters
  set patient(value) {
    this._patient = value;
  }

  set diseases(value) {
    this._diseases = value.map(item => new Disease(item));
  }

  set allergies(value) {
    this._allergies = value.map(item => new Allergy(item));
  }

  set surgery(value) {
    this._surgery = value.map(item => new Surgery(item));
  }

  set diagnostics(value) {
    this._diagnostics = value.map(item => new Diagnostic(item));
  }

  set health(value) {
    this._health = value;
  }

  set notes(value) {
    this._notes = value;
  }

  set bloodType(value) {
    this._bloodType = value;
  }

  set weight(value) {
    this._weight = value;
  }

  set height(value) {
    this._height = value;
  }

  set famillyHistory(value) {
    this._famillyHistory = value;
  }

  set lastUpdate(value) {
    this._lastUpdate = value;
  }

  // Convert to JSON format
  toJson() {
    return JSON.stringify({
      patient: this.patient,
      diseases: this.diseases.map(item => item.toJson()),
      allergies: this.allergies.map(item => item.toJson()),
      surgery: this.surgery.map(item => item.toJson()),
      diagnostics: this.diagnostics.map(item => item.toJson()),
      health: this.health,
      notes: this.notes,
      bloodType: this.bloodType,
      weight: this.weight,
      height: this.height,
      famillyHistory: this.famillyHistory,
      lastUpdate: this.lastUpdate
    });
  }
}

export { MedicalProfile, Disease, Allergy, Surgery, Diagnostic };
