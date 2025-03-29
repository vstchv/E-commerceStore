import { useState } from "react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import CheckoutPage from "./CheckoutPage";

export default function CheckoutWrapper() {
  const [loading, setLoading] = useState(true);

  // if (loading) return <LoadingComponent message="Loading checkout..." />;

  return <CheckoutPage />;
}
