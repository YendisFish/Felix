import './felixbutton.css'

function FelixButton({children, type}) {
    return (
        <div>
            <button className="felix-button" type={type}>{children}</button>
        </div>
    )
}

export default FelixButton;