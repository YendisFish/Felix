import logo from './logo.svg';
import Login from './Components/Login.js';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import HomePage from './pages/HomePage.js'
import LoginPage from './pages/LoginPage.js'

function App() {
  return (
      <Router>
          <Routes>
              <Route path="/" element={<HomePage/>}/>
              <Route path="/login" element={<LoginPage/>}/>
          </Routes>
      </Router>
  );
}

export default App;
