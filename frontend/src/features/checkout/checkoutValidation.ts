import * as yup from 'yup';

export const validationSchema = [
    yup.object({
        fullName: yup.string().required('Full name is required'),
        HomeAddress: yup.string().required('Addres line 1 is required'),
        city: yup.string().required(),
        zip: yup.string().required(),
        country: yup.string().required(),
    }),
    yup.object(),
    yup.object({
        nameOnCard: yup.string().required()
    })
] 