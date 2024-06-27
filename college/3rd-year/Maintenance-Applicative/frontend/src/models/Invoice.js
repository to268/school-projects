class Invoice {
    constructor({
        patientId = '',
        doctorId = '',
        items = [],
        totalAmount = 0,
        status = 'Pending',
        pdf = null,
        createdAt = new Date(),
        updatedAt = new Date()
    } = {}) {
        this._patientId = patientId;
        this._doctorId = doctorId;
        this._items = items.map(item => item instanceof InvoiceItem ? item : new InvoiceItem(item));
        this._totalAmount = totalAmount;
        this._status = status;
        this._pdf = pdf;
        this._createdAt = createdAt;
        this._updatedAt = updatedAt;
    }

    // Getters
    get patientId() {
        return this._patientId;
    }

    get doctorId() {
        return this._doctorId;
    }

    get items() {
        return this._items;
    }

    get totalAmount() {
        return this._totalAmount;
    }

    get status() {
        return this._status;
    }

    get pdf() {
        return this._pdf;
    }

    get createdAt() {
        return this._createdAt;
    }

    get updatedAt() {
        return this._updatedAt;
    }

    // Setters
    set patientId(value) {
        this._patientId = value;
    }

    set doctorId(value) {
        this._doctorId = value;
    }

    set items(value) {
        this._items = value.map(item => new InvoiceItem(item));
    }

    set totalAmount(value) {
        this._totalAmount = value;
    }

    set status(value) {
        this._status = value;
    }

    set pdf(value) {
        this._pdf = value;
    }

    set createdAt(value) {
        this._createdAt = value;
    }

    set updatedAt(value) {
        this._updatedAt = value;
    }

    toJson() {
        return JSON.stringify({
            patientId: this.patientId,
            doctorId: this.doctorId,
            items: this.items.map(item => item.toJson()),
            totalAmount: this.totalAmount,
            status: this.status,
            pdf: this.pdf,
            createdAt: this.createdAt,
            updatedAt: this.updatedAt,
        });
    }
}

class InvoiceItem {
    constructor({
        product = '',
        description = '',
        quantity = 0,
        price = 0,
        total = 0
    } = {}) {
        this._product = product;
        this._description = description;
        this._quantity = quantity;
        this._price = price;
        this._total = total;
    }

    // Getters
    get product() {
        return this._product;
    }

    get description() {
        return this._description;
    }

    get quantity() {
        return this._quantity;
    }

    get price() {
        return this._price;
    }

    get total() {
        return this._total;
    }

    // Setters
    set product(value) {
        this._product = value;
    }

    set description(value) {
        this._description = value;
    }

    set quantity(value) {
        this._quantity = value;
    }

    set price(value) {
        this._price = value;
    }

    set total(value) {
        this._total = value;
    }
    toJson() {
        return {
            product: this.product,
            description: this.description,
            quantity: this.quantity,
            price: this.price,
            total: this.total,
        }
    }
}

export { Invoice, InvoiceItem };
