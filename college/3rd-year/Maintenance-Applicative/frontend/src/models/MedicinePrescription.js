class MedicinePrescription {
    constructor({
        medication = '',
        dosage = '',
        frequency = '',
        duration = '',
    } = {}) {
        this._name = name;
        this._dosage = dosage;
        this._frequency = frequency;
        this._duration = duration;
    }

    // Getters
    get name() {
        return this._name;
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

    
    // Setters
    set name(value) {
        this._name = value;
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
}

export {MedicinePrescription};