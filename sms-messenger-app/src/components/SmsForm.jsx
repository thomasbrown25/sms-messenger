import { useState } from 'react'
import { sendSms } from '../services/smsApi'

const MAX_SMS_LENGTH = 160

function SmsForm() {
  const [phoneNumber, setPhoneNumber] = useState('')
  const [message, setMessage] = useState('')
  const [status, setStatus] = useState(null) // { type: 'success' | 'error', text: string }
  const [loading, setLoading] = useState(false)

  function formatPhoneInput(value) {
    // Strip all non-digits
    const digits = value.replace(/\D/g, '')
    // Format as (XXX) XXX-XXXX
    if (digits.length <= 3) return digits
    if (digits.length <= 6) return `(${digits.slice(0, 3)}) ${digits.slice(3)}`
    return `(${digits.slice(0, 3)}) ${digits.slice(3, 6)}-${digits.slice(6, 10)}`
  }

  function handlePhoneChange(e) {
    const formatted = formatPhoneInput(e.target.value)
    setPhoneNumber(formatted)
  }

  function getRawDigits(formatted) {
    return formatted.replace(/\D/g, '')
  }

  async function handleSubmit(e) {
    e.preventDefault()
    setStatus(null)

    const digits = getRawDigits(phoneNumber)
    if (digits.length !== 10) {
      setStatus({ type: 'error', text: 'Please enter a valid 10-digit US phone number.' })
      return
    }

    const toPhoneNumber = `+1${digits}`

    setLoading(true)
    try {
      await sendSms({ toPhoneNumber, message })
      setStatus({ type: 'success', text: `Message sent to ${toPhoneNumber}` })
      setPhoneNumber('')
      setMessage('')
    } catch (err) {
      setStatus({ type: 'error', text: err.message })
    } finally {
      setLoading(false)
    }
  }

  const charsRemaining = MAX_SMS_LENGTH - message.length

  return (
    <form onSubmit={handleSubmit} noValidate>
      {status && (
        <div className={`alert alert-${status.type === 'success' ? 'success' : 'danger'} alert-dismissible`} role="alert">
          {status.text}
          <button type="button" className="btn-close" onClick={() => setStatus(null)} aria-label="Close" />
        </div>
      )}

      <div className="mb-3">
        <label htmlFor="phoneNumber" className="form-label fw-semibold">
          To
        </label>
        <div className="input-group">
          <span className="input-group-text">+1</span>
          <input
            id="phoneNumber"
            type="tel"
            className="form-control"
            placeholder="(555) 000-0000"
            value={phoneNumber}
            onChange={handlePhoneChange}
            maxLength={14}
            required
            autoComplete="tel-national"
          />
        </div>
        <div className="form-text">US numbers only</div>
      </div>

      <div className="mb-3">
        <label htmlFor="message" className="form-label fw-semibold">
          Message
        </label>
        <textarea
          id="message"
          className="form-control"
          rows={4}
          placeholder="Type your message..."
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          maxLength={1600}
          required
        />
        <div className={`form-text text-end ${charsRemaining < 0 ? 'text-danger' : ''}`}>
          {message.length}/{MAX_SMS_LENGTH}
          {message.length > MAX_SMS_LENGTH && (
            <span className="ms-1 text-warning">
              ({Math.ceil(message.length / MAX_SMS_LENGTH)} segments)
            </span>
          )}
        </div>
      </div>

      <div className="d-grid">
        <button
          type="submit"
          className="btn btn-primary"
          disabled={loading || !phoneNumber || !message}
        >
          {loading ? (
            <>
              <span className="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true" />
              Sending…
            </>
          ) : (
            'Send Message'
          )}
        </button>
      </div>
    </form>
  )
}

export default SmsForm
