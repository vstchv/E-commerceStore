import { Route, Switch } from "react-router";
import { ToastContainer } from "react-toastify";
import AboutPage from "../../features/about/AboutPage";
import Catalog from "../../features/catalog/Catalog";
import ProductDetails from "../../features/catalog/ProductDetails";
import ContactPage from "../../features/contact/ContactPage";
import HomePage from "../../features/home/HomePage";
import Header from "./Header";
import "react-toastify/dist/ReactToastify.css";
import ServerError from "../errors/ServerError";
import NotFound from "../errors/NotFound";
import CartPage from "../../features/cart/CartPage";
import LoadingComponent from "./LoadingComponent";
import { useAppDispatch } from "../store/configureStore";
import { fetchCartAsync } from "../../features/cart/cartSlice";
import Login from "../../features/account/Login";
import Register from "../../features/account/Register";
import { fetchCurrentUser } from "../../features/account/accountSlice";
import PrivateRoute from "./PrivateRoute";
import Orders from "../../features/orders/Orders";
import CheckoutWrapper from "../../features/checkout/CheckoutWrapper";
import Inventory from "../../features/admin/Inventory";
import {
  Container,
  createTheme,
  CssBaseline,
  ThemeProvider,
} from "@mui/material";
import { useState, useCallback, useEffect } from "react";
import "react-toastify/dist/ReactToastify.min.css";

function App() {
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  const initApp = useCallback(async () => {
    try {
      await dispatch(fetchCurrentUser());
      await dispatch(fetchCartAsync());
    } catch (error) {
      console.log(error);
    }
  }, [dispatch]);

  useEffect(() => {
    initApp().then(() => setLoading(false));
  }, [initApp]);

  if (loading) return <LoadingComponent message="Initialising app..." />;

  return (
    <>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored" />
      <CssBaseline />
      <Header />
      <Route exact path="/" component={HomePage} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Container sx={{ mt: 4 }}>
            <Switch>
              <Route exact path="/catalog" component={Catalog} />
              <Route path="/catalog/:id" component={ProductDetails} />
              <Route path="/about" component={AboutPage} />
              <Route path="/contact" component={ContactPage} />
              <Route path="/server-error" component={ServerError} />
              <Route path="/cart" component={CartPage} />
              <PrivateRoute path="/checkout" component={CheckoutWrapper} />
              <PrivateRoute path="/orders" component={Orders} />
              <PrivateRoute
                roles={["Admin"]}
                path="/inventory"
                component={Inventory}
              />
              <Route path="/login" component={Login} />
              <Route path="/register" component={Register} />
              <Route component={NotFound} />
            </Switch>
          </Container>
        )}
      />
    </>
  );
}

export default App;
