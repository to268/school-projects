class User {
    constructor({
      firstName = '',
      lastName = '',
      email = '',
      password = '',
      birthDate = new Date(),
      activated = false,
      secretToken = '',
      role = '',
      sex = '',
      address = {},
      phone = { mobile: [], landline: [] },
      diploma = [],
      diplomaImg = '',
      rating = 0,
      mainSpecialty = '',
      otherSpecialties = [],
      subscribers = [],
      socials = {},
      date = new Date(),
      cv = '',
      bio = '',
      job = '',
      tags = [],
      avatar = '',
      availability = [],
      video = '',
      reviews = [],
      invoices = []
    } = {}) {
      this._firstName = firstName;
      this._address = address;
      this._lastName = lastName;
      this._email = email;
      this._password = password;
      this._birthDate = birthDate;
      this._activated = activated;
      this._secretToken = secretToken;
      this._role = role;
      this._sex = sex;
      this._phone = phone;
      this._diploma = diploma;
      this._diplomaImg = diplomaImg;
      this._rating = rating;
      this._mainSpecialty = mainSpecialty;
      this._otherSpecialties = otherSpecialties;
      this._subscribers = subscribers;
      this._socials = socials;
      this._date = date;
      this._cv = cv;
      this._bio = bio;
      this._job = job;
      this._tags = tags;
      this._avatar = avatar;
      this._availability = availability;
      this._video = video;
      this._reviews = reviews;
      this._invoices = invoices;
    }
    // Getters
    get firstName() {
      return this._firstName;
    }
  
    get lastName() {
      return this._lastName;
    }
  
    get email() {
      return this._email;
    }
  
    get password() {
      return this._password;
    }
  
    get birthDate() {
      return this._birthDate;
    }
  
    get activated() {
      return this._activated;
    }
  
    get secretToken() {
      return this._secretToken;
    }
  
    get role() {
      return this._role;
    }
  
    get sex() {
      return this._sex;
    }
  
    // Setters
    set firstName(value) {
      this._firstName = value;
    }
  
    set lastName(value) {
      this._lastName = value;
    }
  
    set email(value) {
      this._email = value;
    }
  
    set password(value) {
      this._password = value;
    }
  
    set birthDate(value) {
      this._birthDate = value;
    }
  
    set activated(value) {
      this._activated = value;
    }
  
    set secretToken(value) {
      this._secretToken = value;
    }
  
    set role(value) {
      this._role = value;
    }
  
    set sex(value) {
      this._sex = value;
    }

    get address() { return this._address; }
    set address(value) { this._address = value; }

    get phone() { return this._phone; }
    set phone(value) { this._phone = value; }

    get diploma() { return this._diploma; }
    set diploma(value) { this._diploma = value; }

    get diplomaImg() { return this._diplomaImg; }
    set diplomaImg(value) { this._diplomaImg = value; }

    get rating() { return this._rating; }
    set rating(value) { this._rating = value; }

    get mainSpecialty() { return this._mainSpecialty; }
    set mainSpecialty(value) { this._mainSpecialty = value; }

    get otherSpecialties() { return this._otherSpecialties; }
    set otherSpecialties(value) { this._otherSpecialties = value; }

    get subscribers() { return this._subscribers; }
    set subscribers(value) { this._subscribers = value; }

    get socials() { return this._socials; }
    set socials(value) { this._socials = value; }

    get date() { return this._date; }
    set date(value) { this._date = value; }

    get cv() { return this._cv; }
    set cv(value) { this._cv = value; }

    get bio() { return this._bio; }
    set bio(value) { this._bio = value; }

    get job() { return this._job; }
    set job(value) { this._job = value; }

    get tags() { return this._tags; }
    set tags(value) { this._tags = value; }

    get avatar() { return this._avatar; }
    set avatar(value) { this._avatar = value; }

    get availability() { return this._availability; }
    set availability(value) { this._availability = value; }

    get video() { return this._video; }
    set video(value) { this._video = value; }

    get reviews() { return this._reviews; }
    set reviews(value) { this._reviews = value; }

    get invoices() { return this._invoices; }
    set invoices(value) { this._invoices = value; }

    toJson() {
      return {
          firstName: this.firstName,
          lastName: this.lastName,
          email: this.email,
          password: this.password,
          birthDate: this.birthDate,
          role: this.role,
          sex: this.sex,
          user: this.user,
          address: this.address,
          phone: this.phone,
          diploma: this.diploma,
          diplomaImg: this.diplomaImg,
          rating: this.rating,
          mainSpecialty: this.mainSpecialty,
          otherSpecialties: this.otherSpecialties,
          subscribers: this.subscribers,
          socials: this.socials,
          date: this.date,
          cv: this.cv,
          bio: this.bio,
          job: this.job,
          tags: this.tags,
          avatar: this.avatar,
          availability: this.availability,
          video: this.video,
          reviews: this.reviews,
          invoices: this.invoices
      };
  }
  toJsonStringify() {
    return JSON.stringify({
        firstName: this.firstName,
        lastName: this.lastName,
        email: this.email,
        password: this.password,
        birthDate: this.birthDate,
        role: this.role,
        sex: this.sex,
        user: this.user,
        address: this.address,
        phone: this.phone,
        diploma: this.diploma,
        diplomaImg: this.diplomaImg,
        rating: this.rating,
        mainSpecialty: this.mainSpecialty,
        otherSpecialties: this.otherSpecialties,
        subscribers: this.subscribers,
        socials: this.socials,
        date: this.date,
        cv: this.cv,
        bio: this.bio,
        job: this.job,
        tags: this.tags,
        avatar: this.avatar,
        availability: this.availability,
        video: this.video,
        reviews: this.reviews,
        invoices: this.invoices
    });
}
  }
  
  export { User };
  