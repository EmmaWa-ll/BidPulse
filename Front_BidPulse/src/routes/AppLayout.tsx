import { Outlet } from "react-router";
import Header from "./Header";

//APPLAYOUT = Ramen runt vissa specifika sidor- Betsämemr när header skall visas! (då den ej ska visas i login elelr register)
//Outlet = Häär ska sidan visas- (“Visa den sida som matchar URL:en just här.”)
const AppLayout = () => {
  return (
    <>
      <Header />
      <Outlet />
    </>
  );
};
export default AppLayout;
