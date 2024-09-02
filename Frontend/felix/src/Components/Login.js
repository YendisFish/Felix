import {useState} from "react";

function Login() {
    const [inputPassword, setInputPassword] = useState('');
    
    const handleInputPassword = (event) => {
        setInputPassword(event.target.value);
    };
    
    const onButton = () => {
        console.log(inputPassword);
    };
    
    return (
        <div>
            <p>Password:</p>
            <input value={inputPassword} onChange={handleInputPassword}></input>
            <button onClick={onButton}>Submit</button>
        </div>
    )
}

export default Login;