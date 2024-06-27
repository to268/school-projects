class Payment {
    constructor({
        invoiceId = '',
        amount = 0,
        method = '',
        status = 'Pending',
        transactionId = '',
        createdAt = new Date()
    } = {}) {
        this._invoiceId = invoiceId;
        this._amount = amount;
        this._method = method;
        this._status = status;
        this._transactionId = transactionId;
        this._createdAt = createdAt;
    }

    // Getters
    get invoiceId() {
        return this._invoiceId;
    }

    get amount() {
        return this._amount;
    }

    get method() {
        return this._method;
    }

    get status() {
        return this._status;
    }

    get transactionId() {
        return this._transactionId;
    }

    get createdAt() {
        return this._createdAt;
    }

    // Setters
    set invoiceId(value) {
        this._invoiceId = value;
    }

    set amount(value) {
        this._amount = value;
    }

    set method(value) {
        this._method = value;
    }

    set status(value) {
        this._status = value;
    }

    set transactionId(value) {
        this._transactionId = value;
    }

    set createdAt(value) {
        this._createdAt = value;
    }
}

export { Payment };
