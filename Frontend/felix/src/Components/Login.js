import { useState } from 'react';
import './login.css';


function Login() {
    const [inputPassword, setInputPassword] = useState('');
    const [inputUsername, setInputUsername] = useState('');
    
    const handleInputPassword = (event) => {
        setInputPassword(event.target.value);
    };
    
    const handleInputUsername = (event) => {
        setInputUsername(event.target.value);
    };
    
    const handleSubmit = (event) => {
        event.preventDefault();
        console.log('Username:', inputUsername);
        console.log('Password:', inputPassword);
    };
    
    return (
        <div className="login-page">
            <header> 
                <h1>Felix</h1>
            </header>
        
            <div className="login-form">
                <form onSubmit={handleSubmit}>
                    <h2 className='login-title'>Login</h2>
                    <input value={inputUsername} onChange={handleInputUsername} placeholder="Username"/>
                    

                    <input type="password" value={inputPassword} onChange={handleInputPassword} placeholder="Password" />

                    <button type="submit">Sign In</button>
                </form>
            </div>
        </div>
    );
}

export default Login;
