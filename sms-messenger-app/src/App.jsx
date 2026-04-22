import { useState } from 'react'
import SmsForm from './components/SmsForm'

function App() {
  return (
    <div className="min-vh-100 bg-light d-flex align-items-center">
      <div className="container">
        <div className="row justify-content-center">
          <div className="col-12 col-sm-10 col-md-7 col-lg-5">
            <div className="card shadow-sm">
              <div className="card-header bg-primary text-white">
                <h5 className="mb-0">SMS Messenger</h5>
              </div>
              <div className="card-body">
                <SmsForm />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default App
