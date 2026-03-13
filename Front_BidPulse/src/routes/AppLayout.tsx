import { Outlet } from "react-router";
import Header from "./Header";

//APPLAYOUT = Ramen runt vissa specifika sidor- Betsämemr när header skall visas! (då den ej ska visas i login elelr register)

const AppLayout = () => {
  return (
    <>
      <Header />
      <Outlet />
    </>
  );
};
export default AppLayout;
