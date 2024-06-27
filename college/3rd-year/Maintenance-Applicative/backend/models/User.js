const mongoose = require('mongoose')

const UserSchema = new mongoose.Schema({
  firstName: {
    type: String,
    required: true,
  },
  lastName: {
    type: String,
    required: true,
  },
  email: {
    type: String,
    required: true,
    unique: true,
  },
  password: {
    type: String,
    required: true,
  },
  birthDate: {
    type: Date,
    required: true,
  },
  activated: {
    type: Boolean,
    default: false,
  },
  secretToken: {
    type: String,
  },
  role: {
    type: String,
    required: true,
  },
  sex: {
    type: String,
    enum: ['Homme', 'Femme', 'Autre'],
    required: true,
  },
  address: {
    road: { type: String },
    city: { type: String },
    postalCode: { type: String },
    state: { type: String },
  },
  phone: {
    mobile: {
      type: [String],
      required: true,
    },
    landline: {
      type: [String],
    },
  },
  diploma: {
    type: [String],
  },
  diplomaImg: {
    type: String,
  },
  rating: {
    type: Number,
  },
  mainSpecialty: {
    type: String,
  },
  otherSpecialties: {
    type: [String],
  },
  subscribers: [
    {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'user',
    },
  ],
  socials: {
    youtube: {
      type: String,
    },
    twitter: {
      type: String,
    },
    facebook: {
      type: String,
    },
    linkedin: {
      type: String,
    },
    instagram: {
      type: String,
    },
    tiktok: {
      type: String,
    },
  },
  date: {
    type: Date,
    default: Date.now,
  },
  cv: {
    type: String,
  },
  bio: {
    type: String,
  },
  job: {
    type: String,
  },
  tags: {
    type: [String],
  },
  avatar: {
    type: String,
  },
  availability: [
    {
      startDate: {
        type: Date,
      },
      endDate: {
        type: Date,
      },
    },
  ],
  video: {
    type: String,
  },
  reviews: [
    {
      user: {
        type: mongoose.Schema.Types.ObjectId,
        ref: 'user',
      },
      description: {
        type: String,
        required: true,
      },
      comments: [
        {
          user: {
            type: mongoose.Schema.Types.ObjectId,
            ref: 'user',
          },
          text: {
            type: String,
            required: true,
          },
          date: {
            type: Date,
            default: Date.now,
          },
        },
      ],
      date: {
        type: Date,
        default: Date.now,
      },
      type: {
        type: String,
      },
    },
  ],
  invoices: [
    {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'invoice',
    },
  ],
  office: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'office',
    default: new mongoose.Types.ObjectId('667727b3ec439f14d1ce47af'),
  },
})

module.exports = User = mongoose.model('user', UserSchema)
