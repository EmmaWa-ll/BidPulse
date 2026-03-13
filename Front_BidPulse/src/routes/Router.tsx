import { Routes, Route, Navigate } from "react-router";
import AppLayout from "./AppLayout";
import Auctions from "../Pages/Auctions";
import MyAuctions from "../Pages/MyAuctions";
import CreateAuction from "../Pages/CreateAuction";
import Login from "../Pages/Login";
import Register from "../Pages/Register";
import AuctionDetails from "../Pages/AuctionDetails";

//ROUTER = Växelspaket, vilken sida skall visas?
//Tittar på url pch bestämemr vilken page som ska visas-

const Router = () => {
  return (
    <Routes>
      {/* Startsid */}
      <Route path="/" element={<Navigate to="/auctions" replace />} />

      {/* utan header */}
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />

      {/* med header */}
      <Route element={<AppLayout />}>
        <Route path="/auctions" element={<Auctions />} />
        <Route path="/auctions/:id" element={<AuctionDetails />} />
        <Route path="/myauctions" element={<MyAuctions />} />
        <Route path="/createauction" element={<CreateAuction />} />
      </Route>
    </Routes>
  );
};

export default Router;
