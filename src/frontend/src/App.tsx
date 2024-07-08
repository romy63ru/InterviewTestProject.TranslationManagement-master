import React from "react";
import { css } from "@emotion/react";
import { Header } from "./Header";
import "./App.css";
import { HomePage } from "./HomePage";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import NewJobPage from "./NewJobPage";
import { fontFamily, fontSize, gray2 } from "./Styles";

function App() {
  return (
    <BrowserRouter>
      <Header />
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/new" element={<NewJobPage />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
