import './App.css'
import ImageTable from './partials/ImageTable'
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from './pages/Home';
import Login from './pages/Login'
import Header from './partials/Header';
import CreatePhoto from './pages/CreatePhoto';
import CreateGallery from './pages/CreateGallery';

function App() {
  return (
    <>
      <Router>
        <Header className="header"></Header>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/createphoto" element={< CreatePhoto />} />
          <Route path="/creategallery" element={< CreateGallery />} />
        </Routes>
      </Router>
    
    </>
  );
}

export default App
