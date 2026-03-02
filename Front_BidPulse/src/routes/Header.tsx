//HEADER = MENYN högst upp, det som syns (ingen logik)

import { useState } from "react";
import { NavLink } from "react-router";
import "./Header.css";
import useAuth from "../hooks/useAuth";
import type { User } from "../types/Types";

const Header = () => {
  const [open, setOpen] = useState(false);
  const { loggedIn, logout } = useAuth();
  const user: User | null = JSON.parse(localStorage.getItem("user") || "null");

  const closeMenu = () => setOpen(false);

  const handleLogout = () => {
    logout();
    closeMenu();
  };

  return (
    <header className="headerMenu">
      <NavLink to="/auctions" className="logo" onClick={closeMenu}>
        BidPulse
      </NavLink>

      <nav className={`nav ${open ? "open" : ""}`}>
        <NavLink to="/auctions" end onClick={closeMenu}>
          Auctions
        </NavLink>
        {loggedIn && (
          <NavLink to="/myauctions" onClick={closeMenu}>
            My Auctions
          </NavLink>
        )}
        {/* logoin i mobil */}
        {!loggedIn && (
          <NavLink to="/login" className="mobileLogin" onClick={closeMenu}>
            Login
          </NavLink>
        )}
        {loggedIn && (
          <button type="button" className="mobileLogout" onClick={handleLogout}>
            Logout
          </button>
        )}
      </nav>

      <div className="rightSide">
        {loggedIn && user && (
          <span className="welcome">Welcome, {user.name}</span>
        )}
        {loggedIn ? (
          <button type="button" className="loginLink" onClick={handleLogout}>
            Logout
          </button>
        ) : (
          <NavLink className="loginLink" to="/login" onClick={closeMenu}>
            Login
          </NavLink>
        )}
      </div>

      <button
        className="burger"
        aria-label="Toggle menu"
        onClick={() => setOpen(!open)}
      >
        ☰
      </button>
    </header>
  );
};

export default Header;
