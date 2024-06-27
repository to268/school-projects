class PrescriptionItem {
    constructor({
        medicine = '',
        dosage = '',
        frequency = '',
        duration = ''
    } = {}) {
        this._medicine = medicine;
        this._dosage = dosage;
        this._frequency = frequency;
        this._duration = duration;
    }

    // Getters
    get medicine() {
        return this._medicine;
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
    set medicine(value) {
        this._medicine = value;
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

    // Convert to JSON format
    toJson() {
        return {
            medicine: this.medicine,
            dosage: this.dosage,
            frequency: this.frequency,
            duration: this.duration
        };
    }
}

class Prescription {
    constructor({
        patientId = '',
        doctorId = '',
        pdf = null,
        date = new Date(),
        medicines = []
    } = {}) {
        this._patientId = patientId;
        this._doctorId = doctorId;
        this._pdf = pdf;
        this._date = date;
        this._medicines = medicines.map(item => item instanceof PrescriptionItem ? item : new PrescriptionItem(item));
    }

    // Getters
    get patientId() {
        return this._patientId;
    }

    get doctorId() {
        return this._doctorId;
    }

    get pdf() {
        return this._pdf;
    }

    get date() {
        return this._date;
    }

    get medicines() {
        return this._medicines;
    }

    // Setters
    set patientId(value) {
        this._patientId = value;
    }

    set doctorId(value) {
        this._doctorId = value;
    }

    set pdf(value) {
        this._pdf = value;
    }

    set date(value) {
        this._date = value;
    }

    set medicines(value) {
        this._medicines = value.map(item => item instanceof PrescriptionItem ? item : new PrescriptionItem(item));
    }

    // Convert to JSON format
    toJson() {
        return JSON.stringify({
            patientId: this.patientId,
            doctorId: this.doctorId,
            date: this.date,
            medicines: this.medicines.map(item => item.toJson())
        });
    }
}

// class Prescription {
//     constructor({
//         patientName = '',
//         doctorName = '',
//         licenseNumber = '',
//         date = new Date(),
//         phoneNumber = '',
//         signatureImage = '',
//         medicines = []
//     } = {}) {
//         this._patientName = patientName;
//         this._doctorName = doctorName;
//         this._licenseNumber = licenseNumber;
//         this._date = date;
//         this._phoneNumber = phoneNumber;
//         this._signatureImage = signatureImage;
//         this._medicines = medicines.map(item => item instanceof PrescriptionItem ? item : new PrescriptionItem(item));
//     }

//     // Getters
//     get patientName() {
//         return this._patientName;
//     }

//     get doctorName() {
//         return this._doctorName;
//     }

//     get licenseNumber() {
//         return this._licenseNumber;
//     }

//     get date() {
//         return this._date;
//     }

//     get phoneNumber() {
//         return this._phoneNumber;
//     }

//     get signatureImage() {
//         return this._signatureImage;
//     }

//     get medicines() {
//         return this._medicines;
//     }

//     // Setters
//     set patientName(value) {
//         this._patientName = value;
//     }

//     set doctorName(value) {
//         this._doctorName = value;
//     }

//     set licenseNumber(value) {
//         this._licenseNumber = value;
//     }

//     set date(value) {
//         this._date = value;
//     }

//     set phoneNumber(value) {
//         this._phoneNumber = value;
//     }

//     set signatureImage(value) {
//         this._signatureImage = value;
//     }

//     set medicines(value) {
//         this._medicines = value.map(item => item instanceof PrescriptionItem ? item : new PrescriptionItem(item));
//     }

//     // Convert to JSON format
//     toJson() {
//         return JSON.stringify({
//             patientName: this.patientName,
//             doctorName: this.doctorName,
//             licenseNumber: this.licenseNumber,
//             date: this.date,
//             phoneNumber: this.phoneNumber,
//             signatureImage: this.signatureImage,
//             medicines: this.medicines.map(item => item.toJson())
//         });
//     }
// }

export { Prescription, PrescriptionItem };
