import { yupResolver } from "@hookform/resolvers/yup";
import { LoadingButton } from "@mui/lab";
import {
  Paper,
  Typography,
  Stepper,
  Step,
  StepLabel,
  Button,
  Box,
} from "@mui/material";
import { useState, useEffect } from "react";
import { useForm, FieldValues, FormProvider } from "react-hook-form";
import agent from "../../app/api/agent";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { clearCart } from "../cart/cartSlice";
import AddressForm from "./AddressForm";
import { validationSchema } from "./checkoutValidation";
import PaymentForm from "./PaymentForm";
import Review from "./Review";

const steps = ["Shipping address", "Review your order", "Payment details"];

export default function CheckoutPage() {
  const [activeStep, setActiveStep] = useState(0);
  const [orderNumber, setOrderNumber] = useState(0);
  const [loading] = useState(false);
  const dispatch = useAppDispatch();
  const { cart } = useAppSelector((state) => state.cart);

  function getStepContent(step: number) {
    switch (step) {
      case 0:
        return <AddressForm />;
      case 1:
        return <Review />;
      case 2:
        return <PaymentForm />;
      default:
        throw new Error("Unknown step");
    }
  }

  const currentValidationSchema = validationSchema[activeStep];

  const methods = useForm({
    mode: "all",
    resolver: yupResolver(currentValidationSchema),
  });

  const handleNext = async (data: FieldValues) => {
    const { nameOnCard, saveAddress, ...shippingAddress } = data;
    console.log("entered");
    if (activeStep === steps.length - 1) {
      try {
        const orderNumber = await agent.Orders.create({
          
          shippingAddress,
        });
        setOrderNumber(orderNumber);
        setActiveStep(activeStep + 1);
        dispatch(clearCart());
        console.log("here");
      } catch (error) {
        console.log(error);
      }
    } else {
      setActiveStep(activeStep + 1);
    }
  };

  const handleBack = () => {
    setActiveStep(activeStep - 1);
  };
  useEffect(() => {
    agent.Account.fetchAddress()
      .then((response) => {
        if (response) {
          // methods.reset({
          //   ...methods.getValues(),
          //   ...Response,
          //   saveAddress: false,
          // });
        }
      })
      .catch(console.log);
  }, [methods]);

  return (
    <FormProvider {...methods}>
      <Paper
        variant="outlined"
        sx={{ my: { xs: 3, md: 6 }, p: { xs: 2, md: 3 } }}
      >
        <Typography component="h1" variant="h4" align="center">
          Checkout
        </Typography>
        <Stepper activeStep={activeStep} sx={{ pt: 3, pb: 5 }}>
          {steps.map((label) => (
            <Step key={label}>
              <StepLabel>{label}</StepLabel>
            </Step>
          ))}
        </Stepper>
        <>
          {activeStep === steps.length ? (
            <>
              <Typography variant="h5" gutterBottom></Typography>
              <Typography variant="subtitle1">
                Your order number is #{orderNumber}. Thank you for your order
              </Typography>
            </>
          ) : (
            <form onSubmit={methods.handleSubmit(handleNext)}>
              {getStepContent(activeStep)}
              <Box sx={{ display: "flex", justifyContent: "flex-end" }}>
                {activeStep !== 0 && (
                  <Button onClick={handleBack} sx={{ mt: 3, ml: 1 }}>
                    Back
                  </Button>
                )}
                <LoadingButton
                  loading={loading}
                  disabled={!methods.formState.isValid}
                  variant="contained"
                  type="submit"
                  sx={{ mt: 3, ml: 1 }}
                >
                  {activeStep === steps.length - 1 ? "Place order" : "Next"}
                </LoadingButton>
              </Box>
            </form>
          )}
        </>
      </Paper>
    </FormProvider>
  );
}
