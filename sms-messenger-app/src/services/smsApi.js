const API_BASE = import.meta.env.VITE_API_BASE_URL || 'https://localhost:5047'

export async function sendSms({ toPhoneNumber, message }) {
  const response = await fetch(`${API_BASE}/api/sms/send`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ toPhoneNumber, message }),
  })

  const data = await response.json()

  if (!response.ok) {
    throw new Error(data.error ?? 'Failed to send message')
  }

  return data
}
