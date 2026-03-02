import { useState } from "react";
import { useNavigate } from "react-router";

//Denna hook ska hantera inloggningsstatus
//UserExist kollar om user finns i LS. Retunerar true/false. Så appen vet om ngt är inloggadnär sidan laddas om.

//useState(userExists) =  skapar en state varaibel loggedIn. Nom du är inloggad är den trua annaras false.
const useAuth = () => {
  const navigate = useNavigate();
  const userExists = localStorage.getItem("user") !== null;
  const [loggedIn, setLoggedIn] = useState(userExists);

  //tar bort anv från locastorage => sätter sedan login till false => navigerar till /auctions
  const logout = () => {
    localStorage.removeItem("user");
    setLoggedIn(false);
    navigate("/auctions");
  };

  return { loggedIn, logout };
};

export default useAuth;
